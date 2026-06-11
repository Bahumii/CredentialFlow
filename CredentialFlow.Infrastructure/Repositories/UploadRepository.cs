using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Domain.Entities;
using CredentialFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CredentialFlow.Infrastructure.Repositories;

public class UploadRepository : IUploadRepository
{
    private readonly CredentialFlowDbContext _dbContext;

    public UploadRepository(CredentialFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Upload upload, CancellationToken cancellationToken)
    {
        await _dbContext.Uploads.AddAsync(
        upload,
        cancellationToken);
    }

    public async Task<Upload?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Uploads
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task UpdateAsync(Upload upload, CancellationToken cancellationToken)
    {
        _dbContext.Uploads.Update(upload);

        return Task.CompletedTask;
    }

    public async Task AddRowsAsync(IEnumerable<UploadRow> rows, CancellationToken cancellationToken)
    {
        await _dbContext.UploadRows.AddRangeAsync(
            rows,
            cancellationToken);
    }

    public async Task<int> GetRowCountAsync(Guid uploadId, CancellationToken cancellationToken)
    {
        return await _dbContext.UploadRows
            .CountAsync(
                x => x.UploadId == uploadId,
                cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}
