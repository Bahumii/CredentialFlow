using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Domain.Entities
{
    public class ProcessingLog
    {
        public Guid Id { get; set; }

        public Guid UploadId { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
