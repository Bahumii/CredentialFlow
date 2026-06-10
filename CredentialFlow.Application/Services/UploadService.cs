using CredentialFlow.Application.DTOs.Uploads;
using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Application.Interfaces.Services;
using CredentialFlow.Domain.Entities;
using CredentialFlow.Domain.Enums;
using CredentialFlow.Application.Interfaces.Jobs;
using Hangfire;
using System.IO;

namespace CredentialFlow.Application.Services;

public class UploadService : IUploadService
{
    private readonly IUploadRepository _uploadRepository;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public UploadService(IUploadRepository uploadRepository, IBackgroundJobClient backgroundJobClient)
    {
        _uploadRepository = uploadRepository;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<UploadResponseDto> UploadAsync(UploadFileRequestDto request,
        CancellationToken cancellationToken)
    {
        var uploadsFolder = Path.Combine(
    Directory.GetCurrentDirectory(),
    "Uploads");

        Directory.CreateDirectory(uploadsFolder);

        var storedFileName =
            $"{Guid.NewGuid()}_{request.FileName}";

        var filePath = Path.Combine(
            uploadsFolder,
            storedFileName);

        using (var fileStream = new FileStream(
            filePath,
            FileMode.Create))
        {
            await request.FileStream.CopyToAsync(
                fileStream,
                cancellationToken);
        }

        var upload = new Upload
        {
            Id = Guid.NewGuid(),

            FileName = request.FileName,

            FilePath = filePath,

            BatchReference =
        $"UPLOAD-{DateTime.UtcNow:yyyyMMddHHmmss}",

            Status = UploadStatus.Queued,

            UploadedAt = DateTime.UtcNow
        };

        await _uploadRepository.AddAsync(
            upload,
            cancellationToken);

        await _uploadRepository.SaveChangesAsync(
            cancellationToken);

        _backgroundJobClient.Enqueue<IUploadProcessor>(
            processor => processor.ProcessUploadAsync(
                upload.Id,
                CancellationToken.None));

        return new UploadResponseDto
        {
            UploadId = upload.Id,
            BatchReference = upload.BatchReference,
            Status = upload.Status.ToString()
        };

    }
}
