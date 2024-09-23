using Microsoft.EntityFrameworkCore;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.BaseTypes;
using Shared.Domain.Models;
using Shared.Infrastructure.Configurations;
using Shared.Infrastructure.Extensions;

namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        private readonly IWorkContext _workContext;
        private readonly ISecretsManagerService _secretManagement;
        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        public AuthDbContext(DbContextOptions options, IWorkContext workContext, ISecretsManagerService secretsManagerService) : base(options)
        {
            _workContext = workContext;
            _secretManagement = secretsManagerService;
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
            //modelBuilder.ApplyConfigurationsFromAssembly(Shared.Infrastructure.AssemblyReference.Assembly); // Shared.Inf
            //modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;port=5434;Database=authdb;User Id=admin;Password=321");
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
