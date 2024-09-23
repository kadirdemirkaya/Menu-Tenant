using Auth.Api;
using Auth.Application;
using Auth.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

using var configuration = builder.Configuration;

builder.Services.AuthApiRegistration();

builder.Services.AuthApplicationRegistration();

builder.Services.AuthInfrastructureServiceRegistrations(configuration);

var app = builder.Build();

app.AuthApiWebApplicationRegistration();

app.AuthInfrastructureWebApplicationRegistration();

app.UseAuthorization();

app.MapControllers();

app.Run();

