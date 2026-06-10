using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CredentialFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUploadProcessingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingCompletedAt",
                table: "Uploads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingStartedAt",
                table: "Uploads",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "ProcessingCompletedAt",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "ProcessingStartedAt",
                table: "Uploads");
        }
    }
}
