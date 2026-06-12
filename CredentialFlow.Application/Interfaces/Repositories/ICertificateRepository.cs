using CredentialFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Application.Interfaces.Repositories;

public interface ICertificateRepository
{
    Task AddAsync(Certificate certificate,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid learnerId, string courseName,
    CancellationToken cancellationToken);
}
