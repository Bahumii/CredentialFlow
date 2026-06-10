using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CredentialFlow.Infrastructure.Persistence.Configurations
{
    public class UploadConfiguration : IEntityTypeConfiguration<Upload>
    {
        public void Configure(EntityTypeBuilder<Upload> builder) 
        { 
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName)
            .HasMaxLength(255)
            .IsRequired();

            builder.Property(x => x.BatchReference)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.BatchReference)
                .IsUnique();

            builder.HasMany(x => x.Rows)
            .WithOne(x => x.Upload)
            .HasForeignKey(x => x.UploadId);

            builder.Property(x => x.Status)
                .HasConversion<int>();
        }
    }
}
