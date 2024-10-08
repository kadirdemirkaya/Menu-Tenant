using Auth.Api;
using Auth.Application;
using Auth.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AuthApiRegistration(configuration);

builder.Services.AuthApplicationRegistration(configuration);

builder.Services.AuthInfrastructureServiceRegistrations(configuration);

builder.Host.UseSerilog();

var app = builder.Build();

app.AuthApiWebApplicationRegistration(configuration);

app.AuthInfrastructureWebApplicationRegistration();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

