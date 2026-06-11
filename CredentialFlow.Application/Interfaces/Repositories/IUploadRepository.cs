using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Domain.Entities;

namespace CredentialFlow.Application.Interfaces.Repositories;

public interface IUploadRepository
{
    Task AddAsync(Upload upload,
        CancellationToken cancellationToken);

    Task<Upload?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken);

    Task UpdateAsync(Upload upload,
    CancellationToken cancellationToken);

    Task AddRowsAsync(IEnumerable<UploadRow> rows,
    CancellationToken cancellationToken);

    Task<int> GetRowCountAsync(Guid uploadId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
    CancellationToken cancellationToken);
}
