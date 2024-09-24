using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Shared.Application.Extensions;
using Shared.Application.Filters;
using Shared.Domain.Models.Configs;

namespace Auth.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthApiRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddLogging();

            services.AddEndpointsApiExplorer();

            SwaggerConfigs swaggerConfigs = configuration.GetOptions<SwaggerConfigs>("SwaggerConfigs");

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerConfigs.Version, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = swaggerConfigs.Title,
                    Version = swaggerConfigs.Version,
                    Description = swaggerConfigs.Description,
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddHttpContextAccessor();

            services.AddScoped<HashPasswordActionFilter>();

            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }

        public static WebApplication AuthApiWebApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            SwaggerConfigs swaggerConfigs = configuration.GetOptions<SwaggerConfigs>("SwaggerConfigs");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerConfigs.Url, $"{swaggerConfigs.Title} {swaggerConfigs.Version}");
            });

            app.UseHttpsRedirection();

            app.MapHealthChecks("/health");

            return app;
        }
    }
}
