using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretManagement;
using Shared.Domain.Models;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;

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
                options.UseNpgsql("Server=localhost;port=5434;Database=authdb;User Id=admin;Password=321");
            });

            return services;
        }
        public static WebApplication AuthInfrastructureWebApplicationRegistration(this WebApplication app)
        {
            app.MiddlewareRegistration();

            return app;
        }
    }
}
