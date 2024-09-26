using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Shared.Stream
{
    public class StreamBus
    {
        private ValueDictionary _valueDictionary;
        private readonly ILogger<StreamBus> _logger;
        private readonly IServiceProvider _serviceProvider;
        public StreamBus(ValueDictionary valueDictionary, ILogger<StreamBus> logger, IServiceProvider serviceProvider)
        {
            _valueDictionary = valueDictionary;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Dictionary<Type, List<Type>> GetHandler() => _valueDictionary._types;

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IStreamEvent
        {
            var eventType = @event.GetType();
            if (_valueDictionary._types.ContainsKey(eventType))
            {
                var handlerTypes = _valueDictionary._types[eventType];

                foreach (var handlerType in handlerTypes)
                {
                    var handler = (IStreamEventHandler<TEvent>)Activator.CreateInstance(handlerType);
                    await handler.StreamHandler(@event);
                }
            }
        }

        public async Task PublishAsync(string eventTypeVal, string dataVal)
        {
            var eventType = GetHandlerWithString(eventTypeVal);

            if (eventType == null)
                throw new ArgumentException($"No handler found for event type: {eventTypeVal}");

            var @event = JsonConvert.DeserializeObject(dataVal, eventType);
            if (@event == null)
                throw new InvalidOperationException($"Failed to deserialize event of type: {eventTypeVal}");

            try
            {
                if (_valueDictionary._types.TryGetValue(eventType, out var handlerTypes))
                {
                    foreach (var handlerType in handlerTypes)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var handler = scope.ServiceProvider.GetService(handlerType);
                            if (handler == null)
                            {
                                _logger.LogError("{DateTime}: Handler of type {handlerType} could not be resolved", DateTime.UtcNow, handlerType.Name);
                                continue;
                            }

                            // Handler'ın implement ettiği interface'i bul
                            var handlerInterface = handler.GetType()
                                .GetInterfaces()
                                .FirstOrDefault(i => i.IsGenericType &&
                                                     i.GetGenericTypeDefinition() == typeof(IStreamEventHandler<>));

                            if (handlerInterface != null)
                            {
                                var method = handlerInterface.GetMethod("StreamHandler");
                                if (method != null)
                                {
                                    var task = (Task)method.Invoke(handler, new object[] { @event });
                                    await task;
                                }
                                else
                                    _logger.LogError("{DateTime}: Method 'StreamHandler' not found in {handlerInterface}", DateTime.UtcNow, handlerInterface.FullName);
                            }
                            else
                                _logger.LogError("{DateTime}: Method 'StreamHandler' not found in {handlerType}", DateTime.UtcNow, handlerType.Name);
                        }
                    }
                }
                else
                    _logger.LogError("{DateTime}: No handlers registered for event type {eventType}", DateTime.UtcNow, eventType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{DateTime}: Error occurred while publishing event {eventType}", DateTime.UtcNow, eventType);
            }
        }

        public Type? GetHandlerWithString(string type)
        {
            foreach (KeyValuePair<Type, List<Type>> kvp in _valueDictionary._types)
            {
                Type eventType = kvp.Key;

                string eventTypeName = eventType.FullName?.Substring(eventType.FullName.LastIndexOf('.') + 1);
                string incomingTypeName = type.Substring(type.LastIndexOf('.') + 1);

                if (string.Equals(eventTypeName, incomingTypeName, StringComparison.OrdinalIgnoreCase))
                    return eventType;
            }
            return null;
        }

        public void AddHandler<TEvent, THandler>()
           where TEvent : IStreamEvent
           where THandler : IStreamEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            var handlerType = typeof(THandler);

            if (!_valueDictionary._types.ContainsKey(eventType))
            {
                _valueDictionary._types[eventType] = new List<Type>();
            }

            if (!_valueDictionary._types[eventType].Contains(handlerType))
            {
                _valueDictionary._types[eventType].Add(handlerType);
            }
        }

        public bool RemoveHandler<TEvent, THandler>()
           where TEvent : IStreamEvent
           where THandler : IStreamEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            var handlerType = typeof(THandler);

            if (_valueDictionary._types.ContainsKey(eventType))
            {
                return _valueDictionary._types[eventType].Remove(handlerType);
            }

            return false;
        }

        public List<Type> GetHandlers<TEvent>() where TEvent : IStreamEvent
        {
            var eventType = typeof(TEvent);
            return _valueDictionary._types.ContainsKey(eventType) ? _valueDictionary._types[eventType] : new List<Type>();
        }
    }
}
