using CredentialFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Domain.Entities
{
    public class UploadRow
    {
        public Guid Id { get; set; }

        public Guid UploadId { get; set; }

        public Upload Upload { get; set; } = null!;

        public int RowNumber { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CourseName { get; set; } = string.Empty;

        public DateTime CompletionDate { get; set; }

        public UploadRowStatus Status { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
