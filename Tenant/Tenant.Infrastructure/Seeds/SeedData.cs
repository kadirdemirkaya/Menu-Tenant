using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecretManagement;
using Shared.Domain.Aggregates.ProductAggregate;
using Tenant.Infrastructure.Data;

namespace Tenant.Infrastructure.Seeds
{
    public class SeedData
    {
        private readonly MenuDbContext _dbContext;
        private ILogger<SeedData> _logger;
        private ISecretsManagerService _secretsManagerService;

        public SeedData(MenuDbContext dbContext, ILogger<SeedData> logger, ISecretsManagerService secretsManagerService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _secretsManagerService = secretsManagerService;
        }
        public async Task<SeedData> SeedDataApply()
        {
            if (!await _dbContext.Set<Menu>().IgnoreQueryFilters().AnyAsync())
            {

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
