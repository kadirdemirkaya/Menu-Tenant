using EventBusDomain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.MenuTest.Events.Event;
using NUnit.MenuTest.Events.Response;
using NUnit.MenuTest.StreamEvents;
using SecretManagement;
using Shared.Stream;
using StackExchange.Redis;

namespace NUnit.MenuTest
{
    public class PackageTest
    {
        private ServiceCollection services;
        private IConfiguration _configuration;

        [OneTimeSetUp]
        public void GlobalInit()
        {
            services = new();
        }

        [SetUp]
        public void Init()
        {
            services.AddEventBus(AssemblyReference.Assembly);

            services.AddSingleton<ISecretsManagerService>(sp =>
            {
                return new AwsSecretsManagerService(_configuration);
            });

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

            services.AddSingleton<RedisStreamService>();
        }

        [OneTimeTearDown]
        public void GlobalCleanup()
        {
            services = new();
        }

        [Test]
        public async Task domain_event_publish_test()
        {
            var serviceProvider = services.BuildServiceProvider();
            var eventBus = serviceProvider.GetRequiredService<EventBus>();

            MenuTestEventResponse response = await eventBus.PublishAsync(new MenuTestEventRequest() { Message = "Test 123" }) as MenuTestEventResponse;

            if (response.Response is not null && response.Response == "Test 123") Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public async Task aws_secret_manaer_service_create_test()
        {
            var serviceProvider = services.BuildServiceProvider();
            var secretManagerService = serviceProvider.GetRequiredService<ISecretsManagerService>();

            string strKey = "secret_key_5";
            string strSecret = "_this_secret_key_";

            bool res = await secretManagerService.CreateSecretAsync(strKey, strSecret);

            Assert.AreEqual(true, res, "secret data created");
        }

        [Test]
        public async Task aws_secret_manaer_service_get_test()
        {
            var serviceProvider = services.BuildServiceProvider();
            var secretManagerService = serviceProvider.GetRequiredService<ISecretsManagerService>();

            string strKey = "secret_key_5";
            string strSecret = "_this_secret_key_";

            string? value = await secretManagerService.GetSecretValueAsStringAsync(strKey);

            Assert.AreEqual(strSecret, value, "expected str value and incoming data are equals");
        }

        [Test]
        public async Task redis_stream_publisher_test()
        {
            var serviceProvider = services.BuildServiceProvider();
            var redisStreamService = serviceProvider.GetRequiredService<RedisStreamService>();

            await redisStreamService.PublishEventAsync(new StreamEvent() { Message = "naifa7ggbdf" }, StreamEnum.AuthApi);

            Assert.Pass();
        }

        [Test]
        public async Task redis_event_handler_test()
        {
            var serviceProvider = services.BuildServiceProvider();
            var redisStreamService = serviceProvider.GetRequiredService<RedisStreamService>();

            await redisStreamService.PublishEventAsync(new ConnectionPoolUpdateStreamEvent() { Message = "naifa7ggbdf" }, StreamEnum.AuthApi);
        }
    }
}
