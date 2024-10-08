using Database.EventGateway;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.DatabaseEventGatewayRegistration(configuration);

builder.Host.UseSerilog();

var app = builder.Build();

app.DatabaseEventGatewayWebApplication();

app.Run();