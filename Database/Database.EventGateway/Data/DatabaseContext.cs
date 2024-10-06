using Microsoft.EntityFrameworkCore;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuDatabaseAggregate;
using Shared.Domain.BaseTypes;
using Shared.Domain.Models;
using Shared.Infrastructure.Configurations;

namespace Database.EventGateway.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IWorkContext _workContext;
        private readonly ISecretsManagerService _secretsManagerService;

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DatabaseContext(DbContextOptions options, IWorkContext workContext, ISecretsManagerService secretsManagerService) : base(options)
        {
            _workContext = workContext;
            _secretsManagerService = secretsManagerService;
        }

        public DbSet<MenuDatabase> Databases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<MenuDatabase>(new MenuDatabaseConfiguration(_workContext));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Database_Url).GetAwaiter().GetResult());
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
