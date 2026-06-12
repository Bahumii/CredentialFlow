using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Application.DTOs.Uploads;

namespace CredentialFlow.Application.Validators;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequestDto>
{
    public UploadFileRequestValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required.");

        RuleFor(x => x.FileName)
            .Must(x =>
                x.EndsWith(".csv",
                    StringComparison.OrdinalIgnoreCase))
            .WithMessage("Only CSV files are supported.");
    }
}
