using Base.Caching;
using Base.Caching.Key;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models.ConnectionPools;
using Tenant.Infrastructure.Data;

namespace Tenant.Infrastructure.Seeds
{
    public class SeedData
    {
        private readonly MenuDbContext _dbContext;
        private ILogger<SeedData> _logger;
        private ISecretsManagerService _secretsManagerService;
        private ICacheManager _cacheManager;
        private IPollyPolicyService _policyService;
        public SeedData(MenuDbContext dbContext, ILogger<SeedData> logger, ISecretsManagerService secretsManagerService, ICacheManager cacheManager, IPollyPolicyService policyService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _secretsManagerService = secretsManagerService;
            _cacheManager = cacheManager;
            _policyService = policyService;
        }
        public async Task<SeedData> SeedDataApply()
        {

            return this;
        }
        public SeedData MigApply()
        {
            try
            {
                _dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
                _logger.LogInformation("Database migration applied successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during database migration: {ex.Message}");
            }

            return this;
        }

        public SeedData MigApplyFromCache()
        {
            List<ConnectionPoolWithCompanyModel> poolCompModels = _policyService.RetryPolicy.Execute(() =>
            {
                return _cacheManager.Get<List<ConnectionPoolWithCompanyModel>>(CacheKey.Create("connectionwithcompany"), () => new List<ConnectionPoolWithCompanyModel>());
            });

            foreach (var poolCompModel in poolCompModels)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MenuDbContext>();
                optionsBuilder.UseNpgsql(poolCompModel.DbUrls);

                using (var context = new MenuDbContext(optionsBuilder.Options))
                {
                    try
                    {
                        context.Database.MigrateAsync().GetAwaiter().GetResult();
                    }
                    catch (PostgresException ex) when (ex.SqlState == "42P07")
                    {
                        Console.WriteLine($"Table already exists, migrations is passed . Connection: {poolCompModel.DbUrls}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Got error: {ex.Message}. Connection: {poolCompModel.DbUrls}");
                    }
                }
            }

            return this;
        }
    }
}
