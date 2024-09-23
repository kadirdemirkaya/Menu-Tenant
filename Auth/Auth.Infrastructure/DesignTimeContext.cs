using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SecretManagement;
using Shared.Domain.Models;
using Shared.Infrastructure.Extensions;

namespace Auth.Infrastructure
{
    //public class DesignTimeContext : IDesignTimeDbContextFactory<AuthDbContext>
    //{
    //    private readonly ISecretsManagerService _secretManagement;
    //    public DesignTimeContext()
    //    {
            
    //    }

    //    public DesignTimeContext(ISecretsManagerService secretManagement)
    //    {
    //        _secretManagement = secretManagement;
    //    }
    //    public AuthDbContext CreateDbContext(string[] args)
    //    {
    //        DbContextOptionsBuilder<AuthDbContext> dbContextOptionsBuilder = new();

    //        string host = _secretManagement.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresAuthHost).GetAwaiter().GetResult();
    //        string port = _secretManagement.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresAuthPort).GetAwaiter().GetResult();
    //        string userName = _secretManagement.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresAuthUserName).GetAwaiter().GetResult();
    //        string password = _secretManagement.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresAuthPassword).GetAwaiter().GetResult();
    //        string databaseName = _secretManagement.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresAuthDatabaseName).GetAwaiter().GetResult();

    //        string str = string.Empty;
    //        str = str.SetDbUrl(host, port, userName, password, databaseName);

    //        dbContextOptionsBuilder.UseNpgsql(str);
    //        return new(dbContextOptionsBuilder.Options);
    //    }
    //}
}
