﻿using Base.Caching;
using Base.Caching.Configurations;
using EventBusDomain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretManagement;
using Serilog;
using Serilog.Events;
using Shared.Domain.Models;

namespace Shared.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            #region Secret management
            services.AddSingleton<ISecretManagementFactory, SecretManagementFactory>();
            services.AddSingleton<ISecretsManagerService, AwsSecretsManagerService>();
            #endregion

            using (ISecretsManagerService secretsManagerService = new AwsSecretsManagerService(configuration))
            {
                #region Seq
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Seq($"http://{secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.Seq)}")
                .CreateLogger();
                #endregion

                #region Cache (maybe it can be found in the services own layer)
                services.AddCaching(configuration, new CacheConfiguration()
                {
                    baseCacheConfiguration = new()
                    {
                        DefaultCacheTime = 60,
                        ShortTermCacheTime = 3
                    },
                    baseDistributedCacheConfiguration = new()
                    {
                        Enabled = true,
                        ConnectionString = secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.Redis).GetAwaiter().GetResult(),
                        InstanceName = "Caching"
                    }
                });
                #endregion

            }



            return services;
        }
    }
}
