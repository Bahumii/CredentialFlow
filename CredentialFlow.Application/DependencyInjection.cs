using CredentialFlow.Application.Interfaces.Services;
using CredentialFlow.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CredentialFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
    this IServiceCollection services)
    {
        services.AddScoped<IUploadService,
            UploadService>();

        return services;
    }
}
