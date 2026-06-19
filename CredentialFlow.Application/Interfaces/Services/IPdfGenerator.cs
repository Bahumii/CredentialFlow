using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Application.Interfaces.Services;

///  Generates certificate for PDF documents
public interface IPdfGenerator
{
    Task<string> GenerateCertificateAsync(string learnerName, string courseName,
        string certificateNumber, CancellationToken cancellationToken);
}
