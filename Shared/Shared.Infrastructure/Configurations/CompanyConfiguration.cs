﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class CompanyConfiguration(IWorkContext _workContext) : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);

            builder.ToTable(Constants.Tables.Company);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => CompanyId.Create(value));

            builder.Property(p => p.TenantId);

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).HasDefaultValue(true);

            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Property(p => p.DatabaseName).HasMaxLength(100);

            builder.HasOne(c => c.AppUser)
                   .WithMany(u => u.Companies)
                   .HasForeignKey(c => c.AppUserId);

            builder.HasOne(c => c.ConnectionPool)
              .WithOne(cp => cp.Company)
              .HasForeignKey<ConnectionPool>(cp => cp.CompanyId);
        }
    }
}
