using Microsoft.EntityFrameworkCore;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.BaseTypes;
using Shared.Infrastructure.Configurations;

namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        private readonly IWorkContext _workContext;

        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions options) : base(options)
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<AppUser>(new UserConfiguration(_workContext));
            modelBuilder.ApplyConfiguration<Company>(new CompanyConfiguration(_workContext));
            modelBuilder.ApplyConfiguration<ConnectionPool>(new ConnectionPoolConfiguration(_workContext));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");
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
    }
}
