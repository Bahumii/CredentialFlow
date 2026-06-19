using CredentialFlow.Application.Interfaces.Repositories;
using CredentialFlow.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CredentialFlow.Application.Interfaces.Jobs;
using CredentialFlow.Infrastructure.Jobs;
using CredentialFlow.Application.Interfaces.Services;
using CredentialFlow.Infrastructure.Services;

namespace CredentialFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services)
    {
        services.AddScoped<IUploadRepository, UploadRepository>();
        services.AddScoped<IUploadProcessor, UploadProcessor>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<ILearnerRepository, LearnerRepository>();
        services.AddScoped<IPdfGenerator, PdfGenerator>();

        return services;
    }
}
