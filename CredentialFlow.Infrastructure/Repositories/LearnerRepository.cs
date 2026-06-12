using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Domain.Entities;
using CredentialFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Infrastructure.Repositories;

public class LearnerRepository : ILearnerRepository
{
    private readonly CredentialFlowDbContext _dbContext;

    public LearnerRepository(
        CredentialFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Learner?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Learners
            .FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken);
    }

    public async Task AddAsync(
        Learner learner,
        CancellationToken cancellationToken)
    {
        await _dbContext.Learners.AddAsync(
            learner,
            cancellationToken);
    }
}
