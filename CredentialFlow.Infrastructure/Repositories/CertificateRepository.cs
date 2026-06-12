using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Domain.Entities;
using CredentialFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace CredentialFlow.Infrastructure.Repositories;

public class CertificateRepository : ICertificateRepository
{
    private readonly CredentialFlowDbContext _dbContext;

    public CertificateRepository(
        CredentialFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Certificate certificate,
        CancellationToken cancellationToken)
    {
        await _dbContext.Certificates.AddAsync(
            certificate,
            cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid learnerId, string courseName,
    CancellationToken cancellationToken)
    {
        return await _dbContext.Certificates
            .AnyAsync(
                x => x.LearnerId == learnerId &&
                     x.CourseName == courseName,
                cancellationToken);
    }
}
