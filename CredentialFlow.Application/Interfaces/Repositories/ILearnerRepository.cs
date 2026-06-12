using CredentialFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Application.Interfaces.Repositories;

public interface ILearnerRepository
{
    Task<Learner?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task AddAsync(
        Learner learner,
        CancellationToken cancellationToken);
}
