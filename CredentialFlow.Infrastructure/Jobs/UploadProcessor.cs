using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Application.Interfaces.Jobs;
using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CredentialFlow.Infrastructure.Jobs;

public class UploadProcessor : IUploadProcessor
{
    private readonly IUploadRepository _uploadRepository;
    private readonly ILogger<UploadProcessor> _logger;

    public UploadProcessor(
        IUploadRepository uploadRepository,
        ILogger<UploadProcessor> logger)
    {
        _uploadRepository = uploadRepository;
        _logger = logger;
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
