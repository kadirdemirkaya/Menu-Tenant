using Auth.Api;
using Auth.Application;
using Auth.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AuthApiRegistration(configuration);

builder.Services.AuthApplicationRegistration(configuration);

builder.Services.AuthInfrastructureServiceRegistrations(configuration);

var app = builder.Build();

app.AuthApiWebApplicationRegistration(configuration);

app.AuthInfrastructureWebApplicationRegistration();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

