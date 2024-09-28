using Database.EventGateway.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SecretManagement;
using Shared.Domain.Aggregates.MenuDatabaseAggregate;
using Shared.Domain.Models;

namespace Database.EventGateway.Seed
{
    public class SeedData
    {
        private readonly DatabaseContext _dbContext;
        private ILogger<SeedData> _logger;
        private ISecretsManagerService _secretsManagerService;

        public SeedData(DatabaseContext dbContext, ILogger<SeedData> logger, ISecretsManagerService secretsManagerService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _secretsManagerService = secretsManagerService;
        }

        public async Task<SeedData> SeedDataApply()
        {
            MenuDatabase menuDatabase = MenuDatabase.Create(await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Host), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Port), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password), Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb_TenantId, true);

            bool anyShared = await _dbContext.Set<MenuDatabase>().IgnoreQueryFilters().AnyAsync(md => md.TenantId == Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb_TenantId);

            if (!anyShared)
            {
                EntityEntry entityEntry = await _dbContext.Set<MenuDatabase>().AddAsync(menuDatabase);

                if (entityEntry.State == EntityState.Added)
                {
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation("{0} : {1} added to database", DateTime.UtcNow, "shareddb");
                    return this;
                }
                _logger.LogError("{0} : {1} not added to database", DateTime.UtcNow, "shareddb");
                return this;
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
