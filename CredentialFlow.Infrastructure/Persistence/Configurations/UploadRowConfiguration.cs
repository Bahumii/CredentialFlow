using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CredentialFlow.Infrastructure.Persistence.Configurations
{
    public class UploadRowConfiguration : IEntityTypeConfiguration<UploadRow>
    {
        public void Configure(EntityTypeBuilder<UploadRow> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UploadId);

            builder.HasIndex(x => x.Email);

            builder.Property(x => x.Email)
                .HasMaxLength(255);

            builder.Property(x => x.FirstName)
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
            .HasMaxLength(100);

            builder.Property(x => x.CourseName)
                .HasMaxLength(200);

            builder.Property(x => x.Status)
                .HasConversion<int>();
        }
    }
}
