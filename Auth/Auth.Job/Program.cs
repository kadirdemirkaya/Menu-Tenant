using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repository;
using Auth.Job;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;
using Shared.Infrastructure;
using Shared.Stream;
using StackExchange.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        var currentDirectory = Directory.GetCurrentDirectory();

        var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

        var builder = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        configuration = builder.Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddHttpContextAccessor();

        services.SeqRegistration(configuration);

        services.SecretManagementRegistration();

        services.AddRedisRegistration(configuration);

        services.AddStreamBus(Auth.Job.AssemblyReference.Assembly);

        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");
        });

        services.AddScoped<IRepository<Company, CompanyId>, Repository<Company, CompanyId>>();

        services.AddScoped<IRepository<ConnectionPool, ConnectionPoolId>, Repository<ConnectionPool, ConnectionPoolId>>();

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var secretsManagerService = sp.GetRequiredService<ISecretsManagerService>();

            return ConnectionMultiplexer.Connect(secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis2).GetAwaiter().GetResult());
        });

        services.CachingRegistration(configuration);

        //-------

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddQuartz(configurator =>
        {
            configurator.UseMicrosoftDependencyInjectionJobFactory();

            JobKey jobKey = new("Auth.Job.Service.Key");
            //JobKey jobCacheKey = new("Auth.Job.Cache.Service.Key");

            configurator.AddJob<DatabaseRegisterJobService>(options => options.WithIdentity(jobKey));
            //configurator.AddJob<DatabaseCacheJobService>(options =>options.WithIdentity(jobCacheKey));

            //configurator.AddTrigger(options => options.ForJob(jobCacheKey)
            //    .StartAt(DateTime.UtcNow)
            //    .WithSimpleSchedule(builder => builder.WithIntervalInHours(2).RepeatForever()));

            configurator.AddTrigger(options => options.ForJob(jobKey)
                .StartAt(DateTime.UtcNow.AddSeconds(10))
                .WithSimpleSchedule(builder => builder.WithIntervalInHours(2).RepeatForever()));

            //configurator.AddJobListener<JobChainingListener>();
        });
    })
    .Build();

await host.RunAsync();