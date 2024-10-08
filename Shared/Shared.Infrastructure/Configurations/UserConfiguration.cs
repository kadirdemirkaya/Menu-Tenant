﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class UserConfiguration(IWorkContext _workContext) : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);

            builder.ToTable(Constants.Tables.User);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => AppUserId.Create(value));

            builder.Property(p => p.TenantId);

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).HasDefaultValue(true);

            builder.Property(p => p.Username);

            builder.Property(p => p.Email);

            builder.Property(p => p.Password);

            builder.Property(p => p.PhoneNumber);

            builder.HasMany(u => u.Companies)
                   .WithOne(u => u.AppUser)
                   .HasForeignKey(u => u.AppUserId);

        }
    }
}
