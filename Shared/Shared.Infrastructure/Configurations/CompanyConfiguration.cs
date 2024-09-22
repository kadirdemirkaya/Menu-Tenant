using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(Constants.Tables.Company);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => CompanyId.Create(value));

            builder.Property(p => p.TenantId).IsRequired();

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(true);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.Property(p => p.DatabaseName).IsRequired().HasMaxLength(100);

            builder.HasOne(c => c.AppUser)
                   .WithMany(u => u.Companies)
                   .HasForeignKey(c => c.AppUserId);

            builder.HasOne(c => c.ConnectionPool)
              .WithOne(cp => cp.Company)
              .HasForeignKey<ConnectionPool>(cp => cp.CompanyId);

            builder.Property(p => p.AppUserId).IsRequired();
        }
    }
}
