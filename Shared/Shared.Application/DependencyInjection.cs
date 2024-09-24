using EventBusDomain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Application.Extensions;
using Shared.Domain.Models.Configs;
using System.Reflection;
using System.Text;

namespace Shared.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection EventBusRegistration(this IServiceCollection services, Assembly assemblies)
        {
            services.AddEventBus(assemblies);

            return services;
        }
        public static IServiceCollection JwtRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtTokenConfig = configuration.GetOptions<JwtTokenConfigs>("JwtTokenConfigs");

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

    }
}
