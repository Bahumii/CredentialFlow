using System;
using System.Collections.Generic;
using System.Text;
using CredentialFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CredentialFlow.Infrastructure.Persistence.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CourseName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.CertificateNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.CertificateNumber)
            .IsUnique();

            builder.HasOne(x => x.Learner)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.LearnerId);
        }
    }
}
