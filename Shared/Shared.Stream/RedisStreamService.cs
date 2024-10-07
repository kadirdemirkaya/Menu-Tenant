using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Domain.Models;
using StackExchange.Redis;
using System.Numerics;

namespace Shared.Stream
{
    public class RedisStreamService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<RedisStreamService> _logger;

        public RedisStreamService(IConnectionMultiplexer redis, ILogger<RedisStreamService> logger)
        {
            _redis = redis;
            _logger = logger;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event, StreamEnum streamEnum, int expirationInMinutes = 60) where TEvent : IStreamEvent
        {
            try
            {
                var db = _redis.GetDatabase();
                var expirationTime = DateTime.UtcNow.AddMinutes(expirationInMinutes).ToString("o");

                await db.StreamAddAsync(streamEnum.ToString(), new NameValueEntry[]
                {
                    new NameValueEntry("EventType", @event.GetType().FullName),
                    new NameValueEntry("Data", JsonConvert.SerializeObject(@event)),
                    new NameValueEntry("ExpirationTime", expirationTime)
                });

                _logger.LogInformation("{DateTime} : Redis stream event is sent", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " {DateTime} : An error occurred while sending the Redis stream event", DateTime.UtcNow);
            }
        }
    }
}
