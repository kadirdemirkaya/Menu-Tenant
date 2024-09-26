using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Shared.Stream
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStreamBus(this IServiceCollection services, Assembly assembly)
        {
            Shared.Stream.ValueDictionary valueDictionary = new(assembly);

            foreach (KeyValuePair<Type, List<Type>> kvp in valueDictionary._types)
            {
                Type key = kvp.Key;
                List<Type> value = kvp.Value;

                foreach (var handler in kvp.Value)
                {
                    var interfaceType = typeof(IStreamEventHandler<>).MakeGenericType(key);
                    services.AddScoped(interfaceType, handler);
                    services.AddScoped(handler);
                }
            }

            services.AddSingleton(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<StreamBus>>();

                return new StreamBus(valueDictionary, logger, sp);
            });


            services.AddHostedService<Worker>();

            return services;
        }
    }
}
