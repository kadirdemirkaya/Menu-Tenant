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
            if (!await _dbContext.Set<ConnectionPool>().IgnoreQueryFilters().Where(cp => cp.DatabaseName == "shareddb").AnyAsync())
            {
                // TODO : purpose the in this section will creating database request in this place and event sending for shared db in database service
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
