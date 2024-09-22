using Base.Caching;
using Base.Caching.Configurations;
using EventBusDomain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Shared.Infrastructure.Services;
using System.Text;

namespace Shared.Infrastructure
{
    // Kendi içerinde kur
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

            #region JWT 
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
            #endregion

            #region Services
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            #endregion

            return services;
        }
    }
}
