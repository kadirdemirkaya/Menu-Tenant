using Auth.Application.Abstractions;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repository;
using Auth.Infrastructure.Seeds;
using Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
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
using Shared.Stream;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthInfrastructureServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.SeqRegistration(configuration);

            services.SecretManagementRegistration();

            services.AddRedisRegistration(configuration);

            services.ServiceRegistration();

            services.AddDatabase();

            services.ApplySeeds(sp =>
            {
                using (var context = sp.GetRequiredService<AuthDbContext>())
                {
                    context.SaveChangesAsync().GetAwaiter().GetResult();
                }
            });

            services.AddServices();

            services.AddStreamEvent();

            return services;
        }

        private static IServiceCollection ApplySeeds(this IServiceCollection services, Action<IServiceProvider> seedAction)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                SeedData seedData;

                var dbContext = serviceProvider.GetRequiredService<AuthDbContext>();
                var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
                var secretManagement = serviceProvider.GetRequiredService<ISecretsManagerService>();

                seedData = new(dbContext, logger, secretManagement);

                seedData.MigApply().SeedDataApply().GetAwaiter().GetResult();

                seedAction(serviceProvider);
            }

            return services;
        }

        private static IServiceCollection AddStreamEvent(this IServiceCollection services)
        {
            services.AddStreamBus(AssemblyReference.Assembly);

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>((sp, options) =>
            {
                options.UseNpgsql("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<AppUser, AppUserId>, Repository<AppUser, AppUserId>>();

            services.AddScoped<IRepository<Company, CompanyId>, Repository<Company, CompanyId>>();

            services.AddScoped<IRepository<ConnectionPool, ConnectionPoolId>, Repository<ConnectionPool, ConnectionPoolId>>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static WebApplication AuthInfrastructureWebApplicationRegistration(this WebApplication app)
        {
            app.MiddlewareRegistration();

            return app;
        }
    }
}
