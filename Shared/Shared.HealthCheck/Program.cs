using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shared.HealthCheck;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDiscoveryClient();

builder.Services.AddServiceDiscovery(o => o.UseConsul());

builder.Services.AddHttpClient("xabarihealthcheck", client =>
{
    client.BaseAddress = new Uri("http://localhost:5286");
})
.AddServiceDiscovery()
.AddRoundRobinLoadBalancer();

builder.Services.AddHealthChecks()
    .AddNpgSql("localhost:5432")
    .AddRedis(
        redisConnectionString: "localhost:6379",
        name: "Redis",
        tags: ["Docker-Compose", "Redis"])
    .AddCheck(
        name: "Auth Api",
        instance: new HealthChecker2("https://localhost:7001"),
        tags: ["Auth.Api", "REST"]
    )
    .AddCheck(
        name: "Tenant Api",
        instance: new HealthChecker2("https://localhost:7100"),
        tags: ["Tenant.Api", "REST"]
    );

builder.Services.AddHealthChecksUI(setupSettings =>
{
    setupSettings.SetHeaderText("Health Check");
    setupSettings.AddHealthCheckEndpoint("Basic Health Check", "/health");
    setupSettings.SetEvaluationTimeInSeconds(10);
    setupSettings.SetApiMaxActiveRequests(2);
}).AddInMemoryStorage();


var app = builder.Build();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/", () => Results.Redirect("/health-ui"));
app.MapGet("/swagger", () => Results.Redirect("/health-ui"));

app.UseHttpsRedirection();

app.UseHealthChecksUI(config => config.UIPath = "/health-ui");

app.Run();


