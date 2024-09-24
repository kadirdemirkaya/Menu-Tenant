using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.Enums;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(Constants.Tables.Product);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => ProductId.Create(value));

            builder.Property(p => p.TenantId);

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).HasDefaultValue(true);

            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Property(p => p.Price).HasDefaultValue(0).HasColumnType("decimal(18,2)").HasPrecision(18, 2);

            builder.Property(p => p.Title).HasDefaultValue("NONE");

            builder.Property(p => p.ProductStatus).HasDefaultValue(ProductStatus.InStock);

            builder.OwnsOne(p => p.ProductDetails, pd =>
            {
                pd.Property(pp => pp.Description).HasColumnName("Product_Description");
                pd.Property(pp => pp.WeightInGrams).HasColumnName("Product_WeightInGrams");
            });

            builder.HasOne(p => p.Menu)
                   .WithMany(m => m.Products)
                   .HasForeignKey(p => p.MenuId);

            builder.Property(p => p.MenuId);
        }
    }
}
