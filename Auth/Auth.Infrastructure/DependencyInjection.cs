using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Abstractions;
using Shared.Infrastructure;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthInfrastructureServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.ServiceRegistration();

            services.SecretManagementRegistration();

            services.SeqRegistration(configuration);

            services.JwtRegistration();

            services.AddDbContext<AuthDbContext>((sp, options) =>
            {
                var _workContext = sp.GetRequiredService<IWorkContext>();

                string host = _workContext.Tenant.Host;
                string name = _workContext.Tenant.Name;
                string userName = _workContext.Tenant.Username;
                string password = _workContext.Tenant.Password;
                string Port = _workContext.Tenant.Port;
                string databaseName = _workContext.Tenant.DatabaseName;
                string connectionString = $"Server={host};port={Port};Database={databaseName};User Id={userName};Password={password}";

                options.UseNpgsql(connectionString);
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
