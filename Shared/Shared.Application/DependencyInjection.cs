using EventBusDomain;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection EventBusRegistration(this IServiceCollection services, Assembly assemblies)
        {
            services.AddEventBus(assemblies);

            return services;
        }
    }
}
