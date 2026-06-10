using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Domain.Enums
{
    public enum UploadStatus
    {
        Pending = 1,
        Queued = 2,
        Processing = 3,
        Completed = 4,
        Failed = 5
    }
}
