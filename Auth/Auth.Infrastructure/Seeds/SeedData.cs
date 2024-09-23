using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Domain.Aggregates.UserAggregate;

namespace Auth.Infrastructure.Seeds
{
    public class SeedData(DbContext _dbContext, ILogger<SeedData> _logger)
    {
        public SeedData SeedDataApply()
        {
          

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
