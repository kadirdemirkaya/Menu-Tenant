using Auth.Application.Abstractions;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Seeds;
using Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Infrastructure;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthInfrastructureServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.SecretManagementRegistration();

            services.ServiceRegistration();

            services.SeqRegistration(configuration);

            services.JwtRegistration();

            services.AddDbContext<AuthDbContext>((sp, options) =>
            {
                options.UseNpgsql("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");
            });

            services.AddScoped<IUserService, UserService>();

            ApplySeeds(services.BuildServiceProvider());

            return services;
        }

        private static void ApplySeeds(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();

            SeedData seedData;
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedData>>();

            seedData = new(dbContext, logger);

            seedData.SeedDataApply().MigApply();

            scope.Dispose();
        }

        public static WebApplication AuthInfrastructureWebApplicationRegistration(this WebApplication app)
        {
            app.MiddlewareRegistration();

            return app;
        }
    }
}
