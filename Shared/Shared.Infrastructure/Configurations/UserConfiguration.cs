using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable(Constants.Tables.User);

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => AppUserId.Create(value));

            builder.Property(p => p.TenantId).IsRequired();

            builder.Property(p => p.UpdatedDateUTC).HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.CreatedDateUTC).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(true);

            builder.Property(p => p.Username);

            builder.Property(p => p.Email).IsRequired();

            builder.Property(p => p.Password).IsRequired();

            builder.Property(p => p.PhoneNumber).IsRequired();

            builder.HasMany(u => u.Companies)
                   .WithOne(u => u.AppUser)
                   .HasForeignKey(u => u.AppUserId);

        }
    }
}
