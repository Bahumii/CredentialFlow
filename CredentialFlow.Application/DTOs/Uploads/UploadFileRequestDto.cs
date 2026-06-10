using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Application.DTOs.Uploads
{
    public class UploadFileRequestDto
    {
        public Stream FileStream { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;
    }
}
