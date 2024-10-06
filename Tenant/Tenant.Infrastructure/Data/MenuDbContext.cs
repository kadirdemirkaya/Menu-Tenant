using Microsoft.EntityFrameworkCore;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.BaseTypes;
using Shared.Domain.Models;
using Shared.Infrastructure.Configurations;

namespace Tenant.Infrastructure.Data
{
    public class MenuDbContext : DbContext
    {
        private readonly IWorkContext _workContext;
        private readonly ISecretsManagerService _secretsManagerService;

        public MenuDbContext()
        {

        }
        public MenuDbContext(DbContextOptions options) : base(options) // !!!
        {
        }
        public MenuDbContext(DbContextOptions options, IWorkContext workContext, ISecretsManagerService secretsManagerService) : base(options) // !!!
        {
            _workContext = workContext;
            _secretsManagerService = secretsManagerService;
        }


        public DbSet<Menu> Menus { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Menu>(new MenuConfiguration(_workContext));
            modelBuilder.ApplyConfiguration<Product>(new ProductConfigurations(_workContext));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Shared_Url).GetAwaiter().GetResult());
            }
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IEntityTenantId>()
                 .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedDateUTC = DateTime.UtcNow;
                else if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedDateUTC = DateTime.UtcNow;
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.UpdatedDateUTC = DateTime.UtcNow;
                    entry.Entity.IsDeleted = true;
                }

                if (_workContext?.Tenant?.TenantId != null || entry.Entity.TenantId == null)
                {
                    entry.Entity.TenantId = _workContext?.Tenant?.TenantId ?? Guid.NewGuid().ToString();
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<IEntityTenantId>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedDateUTC = DateTime.UtcNow;
                else if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedDateUTC = DateTime.UtcNow;
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.UpdatedDateUTC = DateTime.UtcNow;
                    entry.Entity.IsDeleted = true;
                }

                if (_workContext?.Tenant?.TenantId != null || entry.Entity.TenantId == null)
                {
                    entry.Entity.TenantId = _workContext?.Tenant?.TenantId ?? Guid.NewGuid().ToString();
                }
            }

            return base.SaveChanges();
        }
    }
}
