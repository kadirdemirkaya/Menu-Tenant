using Auth.Application;
using Auth.Infrastructure;
using SecretManagement;
using Shared.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

using var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AuthApplicationRegistration();

builder.Services.AuthInfrastructureServiceRegistrations(configuration);

#region TEST
//using (var sp = builder.Services.BuildServiceProvider())
//{
//    using (ISecretsManagerService secretsManagerService = new AwsSecretsManagerService(configuration))
//    {
//        var data = secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentSeq);
//        Console.WriteLine(data.Result);
//    }
//}
#endregion


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.AuthInfrastructureWebApplicationRegistration();

app.UseAuthorization();

app.MapControllers();

app.Run();

