using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.BaseTypes;

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

            modelBuilder.Entity<AppUser>().HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);
            modelBuilder.Entity<Company>().HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);
            modelBuilder.Entity<ConnectionPool>().HasQueryFilter(p => p.TenantId == _workContext.Tenant.TenantId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string host = _workContext.Tenant.Host;
            string name = _workContext.Tenant.Name;
            string userName = _workContext.Tenant.Username;
            string password = _workContext.Tenant.Password;
            string Port = _workContext.Tenant.Port;
            string databaseName = _workContext.Tenant.DatabaseName;
            string connectionString = $"Server={host};port={Port};Database={databaseName};User Id={userName};Password={password}";

            if (!string.IsNullOrEmpty(connectionString))
            {
                if (name.ToLower() == "postgresql")
                    optionsBuilder.UseNpgsql(connectionString);
            }
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<ITenantId>()
                 .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.TenantId = _workContext.Tenant.TenantId;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
