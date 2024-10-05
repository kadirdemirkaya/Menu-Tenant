using EventBusDomain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Tenant.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection TenantApplicationRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEventBus(AssemblyReference.Assembly);

            services.JwtRegistration(configuration);

            services.AddAuthorization();

            return services;
        }
    }
}
