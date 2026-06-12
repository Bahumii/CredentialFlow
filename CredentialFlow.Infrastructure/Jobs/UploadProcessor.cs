using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Application.Interfaces.Jobs;
using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Globalization;
using CredentialFlow.Domain.Entities;


namespace CredentialFlow.Infrastructure.Jobs;

public class UploadProcessor : IUploadProcessor
{
    private readonly IUploadRepository _uploadRepository;
    private readonly ILogger<UploadProcessor> _logger;
    private readonly ILearnerRepository _learnerRepository;
    private readonly ICertificateRepository _certificateRepository;

    public UploadProcessor(IUploadRepository uploadRepository,ILogger<UploadProcessor> logger,         
        ILearnerRepository learnerRepository, ICertificateRepository certificateRepository)
    {
        _uploadRepository = uploadRepository;
        _logger = logger;
        _learnerRepository = learnerRepository;
        _certificateRepository = certificateRepository;
    }

    public async Task ProcessUploadAsync(
        Guid uploadId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Starting processing for upload {UploadId}",
            uploadId);

        var upload = await _uploadRepository.GetByIdAsync(
            uploadId,
            cancellationToken);

        if (upload == null)
        {
            _logger.LogWarning(
                "Upload {UploadId} not found",
                uploadId);

            return;
        }

        try
        {
            upload.Status = UploadStatus.Processing;

            upload.ProcessingStartedAt =
                DateTime.UtcNow;

            await _uploadRepository.SaveChangesAsync(
                cancellationToken);

            if (!File.Exists(upload.FilePath))
            {
                throw new FileNotFoundException(
                    $"File not found: {upload.FilePath}");
            }

            var fileInfo = new FileInfo(upload.FilePath);

            _logger.LogInformation(
                "Processing file {FileName}. Size: {FileSize} bytes",
                fileInfo.Name,
                fileInfo.Length);

            var rows = new List<UploadRow>();

            var lines = await File.ReadAllLinesAsync(
                upload.FilePath,
                cancellationToken);

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                if (values.Length < 5)
                {
                    continue;
                }

                var uploadRow = new UploadRow
                {
                    Id = Guid.NewGuid(),
                    UploadId = upload.Id,
                    RowNumber = i + 1,
                    FirstName = values[0],
                    LastName = values[1],
                    Email = values[2],
                    CourseName = values[3],
                    Status = UploadRowStatus.Valid
                };

                if (DateTime.TryParse(
                    values[4],
                    out var completionDate))
                {
                    uploadRow.CompletionDate = completionDate;
                }
                else
                {
                    uploadRow.Status = UploadRowStatus.Invalid;
                    uploadRow.ErrorMessage =
                        "Invalid completion date";
                }

                rows.Add(uploadRow);
            }

            await _uploadRepository.AddRowsAsync(
                rows,
                cancellationToken);

            await _uploadRepository.SaveChangesAsync(
                cancellationToken);

            foreach (var row in rows.Where(x =>
             x.Status == UploadRowStatus.Valid))
            {
                var learner =
                    await _learnerRepository.GetByEmailAsync(
                        row.Email,
                        cancellationToken);

                if (learner == null)
                {
                    learner = new Learner
                    {
                        Id = Guid.NewGuid(),
                        FirstName = row.FirstName,
                        LastName = row.LastName,
                        Email = row.Email,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _learnerRepository.AddAsync(
                        learner,
                        cancellationToken);

                    await _uploadRepository.SaveChangesAsync(
                        cancellationToken);

                    _logger.LogInformation(
                        "Created learner {Email}",
                        learner.Email);
                }

                //var certificate = new Certificate
                //{
                //    Id = Guid.NewGuid(),
                //    LearnerId = learner.Id,
                //    CourseName = row.CourseName,
                //    CompletionDate = row.CompletionDate,
                //    IssuedAt = DateTime.UtcNow,
                //    CertificateNumber =
                //        $"CERT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8]}"
                //};

                var certificateExists =
                    await _certificateRepository.ExistsAsync(
                        learner.Id,
                        row.CourseName,
                        cancellationToken);

                if (!certificateExists)
                {
                    var certificate = new Certificate
                    {
                        Id = Guid.NewGuid(),
                        LearnerId = learner.Id,
                        CourseName = row.CourseName,
                        CompletionDate = row.CompletionDate,
                        IssuedAt = DateTime.UtcNow,
                        CertificateNumber =
                            $"CERT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8]}"
                    };

                    await _certificateRepository.AddAsync(
                        certificate,
                        cancellationToken);

                    _logger.LogInformation(
                        "Certificate created for {Email}",
                        learner.Email);
                }
                else
                {
                    _logger.LogWarning(
                        "Duplicate certificate prevented for {Email}",
                        learner.Email);
                }

                //row.Status = UploadRowStatus.Processed;

                //await _certificateRepository.AddAsync(
                //    certificate,
                //    cancellationToken);

                row.Status = UploadRowStatus.Processed;
            }

            await _uploadRepository.SaveChangesAsync(
                    cancellationToken);

            upload.TotalRows = rows.Count;

            upload.SuccessfulRows =
                rows.Count(x =>
                    x.Status != UploadRowStatus.Invalid);

            upload.FailedRows =
                rows.Count(x =>
                    x.Status == UploadRowStatus.Invalid);

            upload.Status = UploadStatus.Completed;

            upload.ProcessingCompletedAt =
                DateTime.UtcNow;

            await _uploadRepository.SaveChangesAsync(
                cancellationToken);

            _logger.LogInformation(
                "Upload {UploadId} completed successfully",
                uploadId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Upload {UploadId} failed",
                uploadId);

            upload.Status = UploadStatus.Failed;

            await _uploadRepository.SaveChangesAsync(
                cancellationToken);

            throw;
        }
    }
}
