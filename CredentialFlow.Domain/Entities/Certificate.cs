using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Domain.Entities
{
    public class Certificate
    {
        public Guid Id { get; set; }

        public Guid LearnerId { get; set; }

        public Learner Learner { get; set; } = null!;

        public string CourseName { get; set; } = string.Empty;

        public string CertificateNumber { get; set; } = string.Empty;

        public DateTime CompletionDate { get; set; }

        public DateTime IssuedAt { get; set; }

        public string? PdfPath { get; set; }
    }
}
