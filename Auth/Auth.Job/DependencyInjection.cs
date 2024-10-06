using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;
using Shared.Infrastructure;
using Shared.Stream;
using StackExchange.Redis;

namespace Auth.Job
{
    public static class DependencyInjection
    {
        public static IServiceCollection AuthJobDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.SeqRegistration(configuration);

            services.SecretManagementRegistration();

            services.AddRedisRegistration(configuration);

            services.AddStreamBus(Auth.Job.AssemblyReference.Assembly);

            services.AddDbContext<AuthDbContext>((sp, options) =>
            {
                ISecretsManagerService _secretsManagerService = sp.GetRequiredService<ISecretsManagerService>();
                options.UseNpgsql(_secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Auth_Url).GetAwaiter().GetResult());
            });

            services.AddScoped<IRepository<Company, CompanyId>, Repository<Company, CompanyId>>();

            services.AddScoped<IRepository<ConnectionPool, ConnectionPoolId>, Repository<ConnectionPool, ConnectionPoolId>>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var secretsManagerService = sp.GetRequiredService<ISecretsManagerService>();

                return ConnectionMultiplexer.Connect(secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis2).GetAwaiter().GetResult());
            });

            services.CachingRegistration(configuration);

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.AddQuartz(configurator =>
            {
                configurator.UseMicrosoftDependencyInjectionJobFactory();

                JobKey jobKey = new("Auth.Job.Service.Key");

                configurator.AddJob<DatabaseRegisterJobService>(options => options.WithIdentity(jobKey));

                configurator.AddTrigger(options => options.ForJob(jobKey)
                    .StartAt(DateTime.UtcNow.AddSeconds(10))
                    .WithSimpleSchedule(builder => builder.WithIntervalInHours(2).RepeatForever()));
            });

            return services;
        }
    }
}
