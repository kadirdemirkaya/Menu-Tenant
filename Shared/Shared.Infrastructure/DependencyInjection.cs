using Base.Caching;
using Base.Caching.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SecretManagement;
using Serilog;
using Serilog.Events;
using Shared.Application.Abstractions;
using Shared.Domain.Models;
using Shared.Domain.Models.Configs;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Middlewares;
using Shared.Infrastructure.Services;
using System.Text;

namespace Shared.Infrastructure
{
    public static class DependencyInjection
    {

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

        public static IServiceCollection JwtRegistration(this IServiceCollection services)
        {
            var jwtTokenConfig = services.GetOptions<JwtTokenConfig>("JwtTokenConfig");

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
                        {
                            x.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = jwtTokenConfig.ValidateIssuer,
                                ValidIssuer = jwtTokenConfig.ValidIssuer,
                                ValidateAudience = jwtTokenConfig.ValidateAudience,
                                ValidAudience = jwtTokenConfig.ValidAudience,
                                ValidateLifetime = jwtTokenConfig.ValidateLifetime,
                                ValidateIssuerSigningKey = jwtTokenConfig.ValidateIssuerSigningKey,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.SecretKey)),
                                ClockSkew = TimeSpan.Zero
                            };
                        });

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

            return app;
        }
    }
}
