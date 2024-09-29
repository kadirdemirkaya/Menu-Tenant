using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.BaseTypes;
using Shared.Infrastructure.Configurations;

namespace Tenant.Infrastructure.Data
{
    public class MenuDbContext : DbContext
    {
        private readonly IWorkContext _workContext;

        public MenuDbContext()
        {

        }
        public MenuDbContext(DbContextOptions options) : base(options) // !!!
        {
        }
        public MenuDbContext(DbContextOptions options, IWorkContext workContext) : base(options) // !!!
        {
            _workContext = workContext;
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
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=shareddb;User Id=admin;Password=passw00rd");
            }
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<ITenantId>()
                 .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (_workContext?.Tenant?.TenantId != null || entry.Entity.TenantId == null)
                {
                    entry.Entity.TenantId = _workContext.Tenant.TenantId;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<ITenantId>()
                 .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (_workContext?.Tenant?.TenantId != null || entry.Entity.TenantId == null)
                {
                    entry.Entity.TenantId = _workContext.Tenant.TenantId;
                }
            }

            return base.SaveChanges();
        }
    }
}
