using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Application.Interfaces.Jobs;

public interface IUploadProcessor
{
    Task ProcessUploadAsync(Guid uploadId,
        CancellationToken cancellationToken = default);
}
