using CredentialFlow.Application.DTOs.Uploads;
using CredentialFlow.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CredentialFlow.Api.Controllers;

[ApiController]
[Route("api/uploads")]
public class UploadsController : ControllerBase
{
    private readonly IUploadService _uploadService;

    public UploadsController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null)
            return BadRequest();

        var dto = new UploadFileRequestDto
        {
            FileName = file.FileName,
            FileStream = file.OpenReadStream()
        };

        var result =
            await _uploadService.UploadAsync(
                dto,
                cancellationToken);

        return Ok(result);
    }
}
