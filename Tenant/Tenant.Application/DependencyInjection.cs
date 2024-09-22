using EventBusDomain;
using Microsoft.Extensions.DependencyInjection;

namespace Tenant.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection TenantApplicationRegistration(this IServiceCollection services)
        {
            #region Domain Event
            services.AddEventBus(AssemblyReference.Assembly);
            #endregion

            return services;
        }
    }
}
