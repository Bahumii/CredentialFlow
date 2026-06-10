using CredentialFlow.Application.DTOs.Uploads;

namespace CredentialFlow.Application.Interfaces.Services;

public interface IUploadService
{
    Task<UploadResponseDto> UploadAsync(UploadFileRequestDto request,
        CancellationToken cancellationToken);
}
