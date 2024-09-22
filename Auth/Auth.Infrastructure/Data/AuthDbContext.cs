using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;

namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        private IWorkContext _workContext;
        public AuthDbContext()
        {

        }

        public AuthDbContext(DbContextOptions options, IWorkContext workContext) : base(options)
        {
            _workContext = workContext;
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ConnectionPool> ConnectionPools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Shared.Infrastructure.AssemblyReference.Assembly); // Shared.Inf
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectionString = _tenantService.GetConnectionString();
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                var dbProvider = _tenantService.GetDatabaseProvider();
                if (dbProvider.ToLower() == "mssql")
                    optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = tenantId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
