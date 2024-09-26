using Newtonsoft.Json;
using Shared.Domain.Models;
using StackExchange.Redis;

namespace Shared.Stream
{
    public class RedisStreamService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisStreamService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event, StreamEnum streamEnum, int expirationInMinutes = 60) where TEvent : IStreamEvent
        {
            var db = _redis.GetDatabase();

            var expirationTime = DateTime.UtcNow.AddMinutes(expirationInMinutes).ToString();

            await db.StreamAddAsync(streamEnum.ToString(), new NameValueEntry[]
            {
                new NameValueEntry("EventType", @event.GetType().FullName),
                new NameValueEntry("Data", JsonConvert.SerializeObject(@event)),
                new NameValueEntry("ExpirationTime", expirationTime)
            });
        }
    }
}
