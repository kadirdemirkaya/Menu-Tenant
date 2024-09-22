using EventBusDomain;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthApplicationRegistration(this IServiceCollection services)
        {
            #region Domain Event
            services.AddEventBus(AssemblyReference.Assembly);
            #endregion

            return services;
        }
    }
}
