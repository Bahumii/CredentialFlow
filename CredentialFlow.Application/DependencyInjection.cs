using CredentialFlow.Application.Interfaces.Services;
using CredentialFlow.Application.Services;
using CredentialFlow.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using CredentialFlow.Application.DTOs.Uploads;

namespace CredentialFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUploadService, UploadService>();

        services.AddScoped<IValidator<UploadFileRequestDto>, UploadFileRequestValidator>();

        return services;
    }
}
