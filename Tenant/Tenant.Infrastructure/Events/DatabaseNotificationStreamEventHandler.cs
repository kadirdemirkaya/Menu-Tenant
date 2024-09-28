using Amazon.Runtime.Internal.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Shared.Stream;
using Tenant.Infrastructure.Data;

namespace Tenant.Infrastructure.Events
{
    public class DatabaseNotificationStreamEventHandler(ILogger<DatabaseNotificationStreamEventHandler> _logger) : IStreamEventHandler<DatabaseNotificationStreamEvent>
    {
        public async Task StreamHandler(DatabaseNotificationStreamEvent @event)
        {
            foreach (var connectionString in @event.ConnectionStrings)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MenuDbContext>();
                optionsBuilder.UseNpgsql(connectionString);

                using (var context = new MenuDbContext(optionsBuilder.Options))
                {
                    try
                    {
                        await context.Database.MigrateAsync();
                    }
                    catch (PostgresException ex) when (ex.SqlState == "42P07")
                    {
                        Console.WriteLine($"Table already exists, migrations is passed . Connection: {connectionString}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Got error: {ex.Message}. Connection: {connectionString}");
                    }
                }
            }
        }
    }
}
