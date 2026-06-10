using Microsoft.EntityFrameworkCore;
using CredentialFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialFlow.Infrastructure.Persistence
{
    public class CredentialFlowDbContext : DbContext
    {
        public CredentialFlowDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Upload> Uploads => Set<Upload>();

        public DbSet<UploadRow> UploadRows => Set<UploadRow>();

        public DbSet<Learner> Learners => Set<Learner>();

        public DbSet<Certificate> Certificates => Set<Certificate>();

        public DbSet<ProcessingLog> ProcessingLogs => Set<ProcessingLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CredentialFlowDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
