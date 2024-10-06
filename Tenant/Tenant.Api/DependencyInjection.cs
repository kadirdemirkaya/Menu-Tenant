using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Shared.Application.Extensions;
using Shared.Domain.Models.Configs;
using Shared.Infrastructure.Filters;
using Tenant.Api.Filters;

namespace Tenant.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection TenantApiRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            SwaggerConfigs swaggerConfigs = configuration.GetOptions<SwaggerConfigs>("SwaggerConfigs");

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerConfigs.Version, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = swaggerConfigs.Title,
                    Version = swaggerConfigs.Version,
                    Description = swaggerConfigs.Description,
                });

                //options.OperationFilter<FormFileOperationFilter>();

                //options.MapType<IFormFile>(() => new OpenApiSchema
                //{
                //    Type = "string",
                //    Format = "binary"
                //});

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

            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

            services.AddScoped<PaginationFilter>();

            return services;
        }

        public static WebApplication TenantApiWebApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            app.UseHttpsRedirection();

            app.UseSwagger();

            SwaggerConfigs swaggerConfigs = configuration.GetOptions<SwaggerConfigs>("SwaggerConfigs");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerConfigs.Url, $"{swaggerConfigs.Title} {swaggerConfigs.Version}");
            });

            app.MapHealthChecks("/health");

            return app;
        }
    }
}
