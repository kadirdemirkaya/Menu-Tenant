using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthApplicationRegistration(this IServiceCollection services)
        {
            services.EventBusRegistration(AssemblyReference.Assembly);

            return services;
        }
    }
}
