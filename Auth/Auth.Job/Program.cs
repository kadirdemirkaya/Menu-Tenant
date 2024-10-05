using Auth.Job;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        services.AuthJobDependencyInjection(configuration);
    })
    .Build();

await host.RunAsync();