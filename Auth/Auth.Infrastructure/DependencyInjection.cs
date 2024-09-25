using Auth.Application.Abstractions;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repository;
using Auth.Infrastructure.Seeds;
using Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Infrastructure;
using System.Diagnostics.Metrics;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthInfrastructureServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.SeqRegistration(configuration);

            services.SecretManagementRegistration();

            services.ServiceRegistration();

            services.AddDatabase();

            ApplySeeds(services.BuildServiceProvider());

            services.AddServices();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Company, CompanyId>, Repository<Company, CompanyId>>();

            services.AddScoped<IRepository<ConnectionPool, ConnectionPoolId>, Repository<ConnectionPool, ConnectionPoolId>>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static void ApplySeeds(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            SeedData seedData;
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedData>>();
            var secretManagement = scope.ServiceProvider.GetRequiredService<ISecretsManagerService>();

            seedData = new(dbContext, logger, secretManagement);

            seedData.MigApply().SeedDataApply().GetAwaiter().GetResult();
        }

        public static WebApplication AuthInfrastructureWebApplicationRegistration(this WebApplication app)
        {
            app.MiddlewareRegistration();

            return app;
        }
    }
}
