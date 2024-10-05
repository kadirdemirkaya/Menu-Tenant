using Microsoft.Extensions.Diagnostics.HealthChecks;
using Tenant.Api;
using Tenant.Application;
using Tenant.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.TenantApiRegistration(configuration);

builder.Services.TenantApplicationRegistration(configuration);

builder.Services.TenantInfrastructureRegistration(configuration);


var app = builder.Build();

app.TenantApiWebApplicationRegistration(configuration);

app.AuthInfrastructureWebApplicationRegistration();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
