using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shared.Application.Filters;

namespace Auth.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthApiRegistration(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddLogging();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddHttpContextAccessor();

            services.AddScoped<HashPasswordActionFilter>();

            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }

        public static WebApplication AuthApiWebApplicationRegistration(this WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.MapHealthChecks("/health");

            return app;
        }
    }
}
