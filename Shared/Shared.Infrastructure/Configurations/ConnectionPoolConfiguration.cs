using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class ConnectionPoolConfiguration(IWorkContext _workContext) : IEntityTypeConfiguration<ConnectionPool>
    {
        public void Configure(EntityTypeBuilder<ConnectionPool> builder)
        {
            builder.HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);

            builder.ToTable(Constants.Tables.ConnectionPool);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => ConnectionPoolId.Create(value));

            builder.Property(p => p.TenantId);

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).HasDefaultValue(true);

            builder.Property(p => p.Name).HasMaxLength(150);

            builder.Property(p => p.Host).HasMaxLength(150);

            builder.Property(p => p.Username).HasMaxLength(150);

            builder.Property(p => p.Password).HasMaxLength(150);

            builder.HasOne(cp => cp.Company)
                 .WithOne(c => c.ConnectionPool)
                 .HasForeignKey<ConnectionPool>(cp => cp.CompanyId);

            builder.Property(p => p.CompanyId);
        }
    }
}
