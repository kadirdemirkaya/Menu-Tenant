using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.DatabaseAggregate;
using Shared.Domain.Aggregates.DatabaseAggregate.ValueObjects;
using Shared.Domain.Aggregates.MenuDatabaseAggregate;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public class MenuDatabaseConfiguration(IWorkContext _workContext) : IEntityTypeConfiguration<MenuDatabase>
    {
        public void Configure(EntityTypeBuilder<MenuDatabase> builder)
        {
            builder.HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);

            builder.ToTable(Constants.Tables.MenuDatabase);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => MenuDatabaseId.Create(value));

            builder.Property(p => p.TenantId);

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).HasDefaultValue(true);

            builder.Property(p => p.IsActive).IsRequired();

            builder.Property(p => p.Host).IsRequired();

            builder.Property(p => p.Port).IsRequired();

            builder.Property(p => p.Username).IsRequired();

            builder.Property(p => p.Password).IsRequired();

            builder.Property(p => p.DatabaseName).IsRequired();


        }
    }
}
