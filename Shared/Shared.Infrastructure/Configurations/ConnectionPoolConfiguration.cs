using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class ConnectionPoolConfiguration : IEntityTypeConfiguration<ConnectionPool>
    {
        public void Configure(EntityTypeBuilder<ConnectionPool> builder)
        {
            builder.ToTable(Constants.Tables.ConnectionPool);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => ConnectionPoolId.Create(value));

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(true);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);

            builder.Property(p => p.Host).IsRequired().HasMaxLength(150);

            builder.Property(p => p.Username).IsRequired().HasMaxLength(150);

            builder.Property(p => p.Password).IsRequired().HasMaxLength(150);

            builder.HasOne(cp => cp.Company)
                 .WithOne(c => c.ConnectionPool)
                 .HasForeignKey<ConnectionPool>(cp => cp.CompanyId);

            builder.Property(p => p.CompanyId).IsRequired();
        }
    }
}
