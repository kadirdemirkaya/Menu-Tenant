using Auth.Job;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        configuration = builder.Build();

        var sp = services.BuildServiceProvider();

        #region Unnecassery
        //using (ISecretsManagerService sManagerService = new AwsSecretsManagerService(configuration))
        //{
        //    services.AddDbContext<AuthDbContext>(options =>
        //    {
        //        options.UseNpgsql("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");
        //        options.UseNpgsql($"Server={Constants.Secrets.DevelopmentPOSTGRES_Host};port={Constants.Secrets.DevelopmentPOSTGRES_Port};Database={Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_AuthDb};User Id={Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User};Password={Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password}");
        //    });
        //}

        //services.AddScoped<IRepository<Company, CompanyId>, Repository<Company, CompanyId>>();

        //services.AddScoped<IRepository<ConnectionPool, ConnectionPoolId>, Repository<ConnectionPool, ConnectionPoolId>>();

        //services.SeqRegistration(configuration);

        //services.SecretManagementRegistration();
        #endregion
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddQuartz(configurator =>
        {
            configurator.UseMicrosoftDependencyInjectionJobFactory();

            JobKey jobKey = new("Auth.Job.Service.Key");

            configurator.AddJob<DatabaseRegisterJobService>(options => options.WithIdentity(jobKey));

            TriggerKey triggerKey = new("Auth.Job.Service.Trigger.Key");

            configurator.AddTrigger(options => options.ForJob(jobKey)
                        .WithIdentity(triggerKey)
                        .StartAt(DateTime.UtcNow)
                        .WithSimpleSchedule
                        (
                            builder => builder.WithIntervalInSeconds(30)
                                              .RepeatForever()
                        ));
        });
    })
    .Build();

await host.RunAsync();