using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Infrastructure.Extensions;
using Shared.Stream;

namespace Auth.Job.Events
{
    public class DatabaseCreatedProcessStreamEventHandler(AuthDbContext _context, ILogger<DatabaseRegisterJobService> _logger, StreamBus _streamBus) : IStreamEventHandler<DatabaseCreatedProcessStreamEvent>
    {
        public async Task StreamHandler(DatabaseCreatedProcessStreamEvent @event)
        {
            if (@event.IsDbCreated is true)
            {
                ConnectionPool? connectionPool = await _context.Set<ConnectionPool>().IgnoreQueryFilters().AsNoTracking().FilterByTenant(@event.TenantId).FirstOrDefaultAsync();

                if (connectionPool is not null)
                {
                    connectionPool.SetUpdatedDateUTC(DateTime.UtcNow);
                    connectionPool.SetIsActive(true);

                    _context.Update(connectionPool);
                    await _context.SaveChangesAsync();

                    //
                    // maybe could send a mail for user menu
                    //
                }
                else
                    _logger.LogError("{DateTime} : ConnectionPool is not found in DatabaseCreatedProcessStreamEventHandler ! ", DateTime.UtcNow);
            }
        }
    }
}
