using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CredentialFlow.Infrastructure.Persistence.Configurations
{
    public class ProcessingLogConfiguration : IEntityTypeConfiguration<ProcessingLog>
    {
        public void Configure(EntityTypeBuilder<ProcessingLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .HasMaxLength(1000);

            builder.HasIndex(x => x.UploadId);
        }
    }
}
