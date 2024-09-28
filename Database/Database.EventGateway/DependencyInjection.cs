using Database.EventGateway.Data;
using Database.EventGateway.Seed;
using Database.EventGateway.Services;
using Microsoft.EntityFrameworkCore;
using SecretManagement;
using Shared.Domain.Models;
using Shared.Infrastructure;
using Shared.Stream;
using StackExchange.Redis;

namespace Database.EventGateway
{
    public static class DependencyInjection
    {
        public static IServiceCollection DatabaseEventGatewayRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();

            services.AddDbContext<DatabaseContext>((serviceProvider, options) =>
            {
                ISecretsManagerService secretManagement = serviceProvider.GetRequiredService<ISecretsManagerService>();

                options.UseNpgsql($"Server=localhost;port=5432;Database={secretManagement.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Database).GetAwaiter().GetResult()};User Id=admin;Password=passw00rd");
            });

            services.SeqRegistration(configuration);

            services.SecretManagementRegistration();

            services.AddRedisRegistration(configuration);

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var secretsManagerService = sp.GetRequiredService<ISecretsManagerService>();

                return ConnectionMultiplexer.Connect(secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis2).GetAwaiter().GetResult());
            });

            services.CachingRegistration(configuration);

            services.ServiceRegistration();

            services.AddStreamBus(Database.EventGateway.AssemblyReference.Assembly);

            services.AddScoped<IDatabaseService, DatabaseService>();

            services.ApplySeeds(sp =>
            {
                using (var context = sp.GetRequiredService<DatabaseContext>())
                {
                    context.SaveChangesAsync().GetAwaiter().GetResult();
                }
            });


            return services;
        }

        private static IServiceCollection ApplySeeds(this IServiceCollection services, Action<IServiceProvider> seedAction)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                SeedData seedData;

                var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
                var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
                var secretManagement = serviceProvider.GetRequiredService<ISecretsManagerService>();

                seedData = new(dbContext, logger, secretManagement);

                seedData.MigApply().SeedDataApply().GetAwaiter().GetResult();

                seedAction(serviceProvider);
            }

            return services;
        }

        public static WebApplication DatabaseEventGatewayWebApplication(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.MiddlewareRegistration();

            return app;
        }
    }
}
