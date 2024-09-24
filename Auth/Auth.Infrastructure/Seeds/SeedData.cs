using Amazon.SecretsManager.Model;
using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecretManagement;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Infrastructure.Seeds
{
    public class SeedData
    {
        private readonly AuthDbContext _dbContext;
        private ILogger<SeedData> _logger;
        private ISecretsManagerService _secretsManagerService;

        public SeedData(AuthDbContext dbContext, ILogger<SeedData> logger, ISecretsManagerService secretsManagerService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _secretsManagerService = secretsManagerService;
        }
        public async Task<SeedData> SeedDataApply()
        {
            if (!await _dbContext.Set<AppUser>().IgnoreQueryFilters().AnyAsync())
            {
                string tenantId = Guid.NewGuid().ToString();

                var appUser = AppUser.Create(AppUserId.CreateUnique(), "kadir", "kadir@gmail.com", "kadir123", "5556667788", tenantId);

                var company = Company.Create(CompanyId.CreateUnique(), "kadir_company", "shared", appUser.Id, tenantId);

                var connectionPool = ConnectionPool.Create(ConnectionPoolId.CreateUnique(), "postgresql", await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Host), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Port), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password), company.Id, tenantId);

                company.AddConnectionPool(connectionPool);

                appUser.AddCompany(company);

                await _dbContext.Set<AppUser>().AddAsync(appUser);

                await _dbContext.SaveChangesAsync();
            }

            return this;
        }
        public SeedData MigApply()
        {
            if (_dbContext.Database.EnsureCreated())
                _logger.LogInformation("db database ensured is worked");
            else
            {
                _dbContext.Database.Migrate();
                _logger.LogInformation("db database migrate is worked");
            }

            return this;
        }
    }
}
