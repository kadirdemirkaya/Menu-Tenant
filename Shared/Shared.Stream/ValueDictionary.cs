using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Shared.Stream
{
    public struct ValueDictionary
    {
        private Dictionary<Type, List<Type>> types;
        public Dictionary<Type, List<Type>> _types { get; private set; }
        public ValueDictionary(Assembly assembly)
        {
            types = new();
            DiscoverHandlers(assembly);
            _types = new Dictionary<Type, List<Type>>(types);
        }

        private void DiscoverHandlers(Assembly assembly)
        {
            var eventTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IStreamEvent).IsAssignableFrom(t))
                .ToList();

            foreach (var eventType in eventTypes)
            {
                var handlerType = assembly.GetTypes()
                    .Where(h => h.IsClass && !h.IsAbstract &&
                                h.GetInterfaces()
                                 .Any(i => i.IsGenericType &&
                                           i.GetGenericTypeDefinition() == typeof(IStreamEventHandler<>)
                                           && i.GenericTypeArguments[0] == eventType)).ToList();

                if (handlerType.Any())
                {
                    types[eventType] = handlerType;
                }
            }
        }
    }
}
