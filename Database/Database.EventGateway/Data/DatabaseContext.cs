using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuDatabaseAggregate;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Infrastructure.Configurations;
using System.Reflection.Metadata.Ecma335;

namespace Database.EventGateway.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IWorkContext _workContext;

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DatabaseContext(DbContextOptions options, IWorkContext workContext) : base(options)
        {
            _workContext = workContext;
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
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=database;User Id=admin;Password=passw00rd");
            }
        }
    }
}
