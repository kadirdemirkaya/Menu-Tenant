using Auth.Application;
using Auth.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SecretManagement;
using Shared.Domain.Models;
using Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

using var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AuthApplicationRegistration();

builder.Services.AuthInfrastructureServiceRegistrations(configuration);


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.AuthInfrastructureWebApplicationRegistration();

app.UseAuthorization();

app.MapControllers();

app.Run();

