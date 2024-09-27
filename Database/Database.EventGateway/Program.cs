using Database.EventGateway;
using Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.DatabaseEventGatewayRegistration(configuration);


var app = builder.Build();

app.DatabaseEventGatewayWebApplication();

app.Run();