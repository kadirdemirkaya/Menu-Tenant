using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SecretManagement;
using Shared.Domain.Models;
using Shared.HealthCheck;
using Shared.Infrastructure;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDiscoveryClient();

builder.Services.SecretManagementRegistration();

builder.Services.AddServiceDiscovery(o => o.UseConsul());

builder.Services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

using (ISecretsManagerService secretsManagerService = new AwsSecretsManagerService(builder.Configuration))
{
    builder.Services.AddHttpClient("xabarihealthcheck", client =>
    {
        client.BaseAddress = new Uri(secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentXabarihealthcheck).GetAwaiter().GetResult());
    })
.AddServiceDiscovery()
.AddRoundRobinLoadBalancer();

    builder.Services.AddHealthChecks()
        .AddRedis(
            redisConnectionString: await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentRedis2),
            name: "Redis",
            tags: ["Docker-Compose", "Redis"]
        )
        .AddNpgSql(
            connectionString: await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Auth_Url),
            name: "PostgreSql_AuthDb",
            tags: ["Docker-Compose", "Postgresql", "AuthDB"]
        )
        .AddConsul(setup =>
            {
                setup.HostName = "localhost";
                setup.Port = 8500;
            },
            name: "Consul",
            tags: ["Docker-Compose", "Consul"]
        )
        .AddNpgSql(
            connectionString: await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Database_Url),
            name: "PostgreSql_DatabaseDB",
            tags: ["Docker-Compose", "Postgresql", "DatabaseDB"]
        )
        .AddNpgSql(
            connectionString: await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Shared_Url),
            name: "PostgreSql_SharedDB",
            tags: ["Docker-Compose", "Postgresql", "SharedDB"]
        )
        .AddCheck(
            name: "Auth Api",
            instance: new HealthChecker2(await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentAuthApi)),
            tags: ["Auth.Api", "REST"]
        )
        .AddCheck(
            name: "Database Api",
            instance: new HealthChecker2(await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentDatabaseApi)),
            tags: ["Auth.Api", "Event"]
        )
        .AddCheck(
            name: "Tenant Api",
            instance: new HealthChecker2(await secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentTenantApi)),
            tags: ["Tenant.Api", "REST"]
        );

    builder.Services.AddHealthChecksUI(setupSettings =>
    {
        setupSettings.SetHeaderText("Health Check");
        setupSettings.AddHealthCheckEndpoint("Basic Health Check", "/health");
        setupSettings.SetEvaluationTimeInSeconds(10);
        setupSettings.SetApiMaxActiveRequests(2);
    }).AddInMemoryStorage();
}


var app = builder.Build();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => Results.Redirect("/health-ui"));
app.MapGet("/swagger", () => Results.Redirect("/health-ui"));

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseHealthChecksUI(config => config.UIPath = "/health-ui");

app.Run();


