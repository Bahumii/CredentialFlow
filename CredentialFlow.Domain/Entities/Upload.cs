using CredentialFlow.Domain.Enums;
using CredentialFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Domain.Entities
{
    public class Upload
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public UploadStatus Status { get; set; }

        public int TotalRows { get; set; }

        public int SuccessfulRows { get; set; }

        public int FailedRows { get; set; }

        public DateTime UploadedAt { get; set; }

        public string BatchReference { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime? ProcessingStartedAt { get; set; }

        public DateTime? ProcessingCompletedAt { get; set; }

        public ICollection<UploadRow> Rows { get; set; }
            = new List<UploadRow>();
    }
}
