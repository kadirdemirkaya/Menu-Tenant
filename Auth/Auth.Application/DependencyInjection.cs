using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthApplicationRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.EventBusRegistration(AssemblyReference.Assembly);

            services.JwtRegistration(configuration);

            return services;
        }
    }
}
