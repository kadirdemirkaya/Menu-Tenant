using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;
using Shared.Domain.Models.Configs;
using StackExchange.Redis;

namespace Shared.Stream
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly StreamBus _streamBus;
        private IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConnectionMultiplexer redis, StreamBus streamBus, IConfiguration configuration)
        {
            _logger = logger;
            _redis = redis;
            _db = _redis.GetDatabase();
            _streamBus = streamBus;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StreamConfigs _streamConfigs = _configuration.GetOptions<StreamConfigs>("StreamConfigs");

            _logger.LogInformation("Worker baþladý.");

            try
            {
                await _db.StreamCreateConsumerGroupAsync(_streamConfigs.StreamKey, _streamConfigs.GroupName);
            }
            catch (Exception)
            {
                _logger.LogWarning("{DateTime} Consumer group already exists !", DateTime.UtcNow);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var entries = await _db.StreamReadGroupAsync(
                  _streamConfigs.StreamKey,
                  _streamConfigs.GroupName,
                  _streamConfigs.ConsumerName,
                  count: 1,
                  noAck: false);

                if (entries.Length > 0 && entries is not null)
                {
                    var entry = entries[0];

                    string eventTypeVal = entry.Values[0].Value.ToString();
                    var dataVal = entry.Values[1].Value;

                    #region this value will improved soon 
                    // TODO : var expirationTimeVal = entry.Values[2].Value;
                    #endregion

                    await _streamBus.PublishAsync(eventTypeVal, dataVal);

                    await _db.StreamAcknowledgeAsync(_streamConfigs.StreamKey, _streamConfigs.GroupName, entry.Id);
                }
                else
                {

                }
                await Task.Delay(1000);
            }
            _logger.LogInformation("Worker is stoped.");
        }
    }
}
