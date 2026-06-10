using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Application.DTOs.Uploads
{
    public class UploadResponseDto
    {
        public Guid UploadId { get; set; }

        public string BatchReference { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
