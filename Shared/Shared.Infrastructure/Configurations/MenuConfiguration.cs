using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class MenuConfiguration(IWorkContext _workContext) : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);

            builder.ToTable(Constants.Tables.Menu);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => MenuId.Create(value));

            builder.Property(p => p.TenantId);

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).HasDefaultValue(true);

            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Property(p => p.Description).HasDefaultValue("NONE").HasMaxLength(250);

            builder.Property(p => p.WebUrl).HasDefaultValue("NONE").HasMaxLength(150);

            builder.OwnsOne(p => p.Address, ad =>
            {
                ad.Property(ap => ap.City).HasColumnName("Menu_City");
                ad.Property(ap => ap.Country).HasColumnName("Menu_Country");
                ad.Property(ap => ap.Street).HasColumnName("Menu_Street");
            });

            builder.HasMany(m => m.Products)
                   .WithOne(p => p.Menu)
                   .HasForeignKey(m => m.MenuId);
        }
    }
}
