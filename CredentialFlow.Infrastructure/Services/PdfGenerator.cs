using CredentialFlow.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CredentialFlow.Infrastructure.Services;

// Responsible for generating PDF certicate files and
// stores them on disk for later retrieval
public class PdfGenerator : IPdfGenerator
{
    public Task<string> GenerateCertificateAsync(string learnerName, string courseName,
        string certificateNumber, CancellationToken cancellationToken)
    {
        //ensure certificate output directory exists
        var certificatesFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Certificates");

        Directory.CreateDirectory(certificatesFolder);

        var fileName =
            $"{certificateNumber}.pdf";

        var filePath = Path.Combine(
            certificatesFolder,
            fileName);

        // Create PDF document.
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);

                page.Content()
                    .Column(column =>
                    {
                        column.Spacing(15);

                        column.Item()
                            .Text("Certificate of Completion")
                            .FontSize(28)
                            .Bold();

                        column.Item()
                            .Text("Awarded To");

                        column.Item()
                            .Text(learnerName)
                            .FontSize(22)
                            .Bold();

                        column.Item()
                            .Text("For successful completion of");

                        column.Item()
                            .Text(courseName)
                            .FontSize(18);

                        column.Item()
                            .Text($"Certificate Number: {certificateNumber}");

                        column.Item()
                            .Text(
                                $"Issued: {DateTime.UtcNow:yyyy-MM-dd}");
                    });
            });
        })
        .GeneratePdf(filePath);

        return Task.FromResult(filePath);
    }
}
