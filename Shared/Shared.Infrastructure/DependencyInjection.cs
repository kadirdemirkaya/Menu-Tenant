using Base.Caching;
using Base.Caching.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretManagement;
using Serilog;
using Serilog.Events;
using Shared.Application.Abstractions;
using Shared.Domain.Models;
using Shared.Infrastructure.Middlewares;
using Shared.Infrastructure.Services;
using StackExchange.Redis;

namespace Shared.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPollyPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IPollyPolicyService, PollyPolicyService>();

            return services;
        }

        public static IServiceCollection SecretManagementRegistration(this IServiceCollection services)
        {
            services.AddSingleton<ISecretManagementFactory, SecretManagementFactory>();
            services.AddSingleton<ISecretsManagerService, AwsSecretsManagerService>();

            return services;
        }

        public static IServiceCollection SeqRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            using (ISecretsManagerService secretsManagerService = new AwsSecretsManagerService(configuration))
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Seq($"http://{secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentSeq).GetAwaiter().GetResult()}")
                .CreateLogger();
            }

            return services;
        }

        public static IServiceCollection AddRedisRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            using (ISecretsManagerService secretsManagerService = new AwsSecretsManagerService(configuration))
            {
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis2).GetAwaiter().GetResult()));
            }
            return services;
        }

        public static IServiceCollection CachingRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            using (ISecretsManagerService secretsManagerService = new AwsSecretsManagerService(configuration))
            {
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
                        ConnectionString = secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis).GetAwaiter().GetResult(),
                        InstanceName = "Caching"
                    }
                });
            }
            return services;
        }

        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IWorkContext, WorkContext>();

            return services;
        }

        public static WebApplication MiddlewareRegistration(this WebApplication app)
        {
            app.UseMiddleware<JwtMiddleware>();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseMiddleware<CompanyNameMiddleware>();

            return app;
        }
    }
}
