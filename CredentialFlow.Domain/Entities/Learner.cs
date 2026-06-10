using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Domain.Entities
{
    public class Learner
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ICollection<Certificate> Certificates { get; set; }
            = new List<Certificate>();
    }
}
