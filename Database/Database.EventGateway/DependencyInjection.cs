using Database.EventGateway.Data;
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

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var secretsManagerService = sp.GetRequiredService<ISecretsManagerService>();

                return ConnectionMultiplexer.Connect(secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis2).GetAwaiter().GetResult());
            });

            services.ServiceRegistration();

            services.AddStreamBus(Database.EventGateway.AssemblyReference.Assembly);

            services.AddScoped<IDatabaseService, DatabaseService>();

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
