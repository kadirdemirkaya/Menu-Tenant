using Base.Caching;
using Base.Caching.Key;
using Database.EventGateway.Data;
using Database.EventGateway.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SecretManagement;
using Shared.Domain.Aggregates.MenuDatabaseAggregate;
using Shared.Domain.Models.ConnectionPools;
using Shared.Infrastructure.Extensions;
using Shared.Stream;
using System.Data.SqlClient;

namespace Database.EventGateway.Events
{
    public class ConnectionPoolUpdateStreamEventHandler(ISecretsManagerService _secretsManager, StreamBus _streamBus, RedisStreamService _redisStreamService, DatabaseContext _context, ILogger<ConnectionPoolUpdateStreamEventHandler> _logger, IDatabaseService _databaseService, ICacheManager _cacheManager) : IStreamEventHandler<ConnectionPoolUpdateStreamEvent>
    {
        private string updateConnectionPoolQuery = @"
            UPDATE ""ConnectionPools""
            SET ""DatabaseName"" = @NewDatabaseName
            WHERE ""TenantId"" = @TenantId";

        private string updateCompanyQuery = @"
            UPDATE ""Companies""
            SET ""DatabaseName"" = @NewDatabaseName
            WHERE ""TenantId"" = @TenantId";

        private bool updateConPool = false;
        private bool updateCompany = false;

        public async Task StreamHandler(ConnectionPoolUpdateStreamEvent @event)
        {
            bool anyResponse = await _context.Databases.IgnoreQueryFilters().AsNoTracking().AnyAsync(d => d.DatabaseName == @event.ConnectionPoolUpdate.DatabaseName && d.IsDeleted == false);

            if (!anyResponse)
            {
                await _databaseService.SetConnectionStringAsync("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd");

                MenuDatabase menuDatabase = MenuDatabase.Create(@event.ConnectionPoolUpdate.Host, @event.ConnectionPoolUpdate.Port, @event.ConnectionPoolUpdate.DatabaseName, @event.ConnectionPoolUpdate.Username, @event.ConnectionPoolUpdate.Password, @event.ConnectionPoolUpdate.TenantId);

                if (await _databaseService.CreateDatabaseIfNotExistsAsync(@event.ConnectionPoolUpdate.DatabaseName))
                {
                    _logger.LogInformation("{DateTime} : {Datebase} create process is succesfully.", DateTime.UtcNow, @event.ConnectionPoolUpdate.DatabaseName);

                    using (var connection = new NpgsqlConnection("Server=localhost;port=5432;Database=authdb;User Id=admin;Password=passw00rd"))
                    {
                        await connection.OpenAsync();

                        // Update ConnectionPools
                        using (var command = new NpgsqlCommand(updateConnectionPoolQuery, connection))
                        {
                            command.Parameters.AddWithValue("@NewDatabaseName", @event.ConnectionPoolUpdate.DatabaseName);
                            command.Parameters.AddWithValue("@TenantId", @event.ConnectionPoolUpdate.TenantId);

                            try
                            {
                                if (await command.ExecuteNonQueryAsync() > 0)
                                {
                                    updateConPool = true;
                                    _logger.LogInformation("{DateTime} : ConnectionPool database name updated.", DateTime.UtcNow);
                                }
                                else
                                {
                                    updateConPool = false;
                                    _logger.LogError("{DateTime} : ConnectionPool database name not updated !", DateTime.UtcNow);
                                }
                            }
                            catch (NpgsqlException ex)
                            {
                                _logger.LogError("{DateTime} : Error updating ConnectionPool database name: {Error}", DateTime.UtcNow, ex.Message);
                                updateConPool = false;
                            }
                        }

                        // Update Company
                        using (var command = new NpgsqlCommand(updateCompanyQuery, connection))
                        {
                            command.Parameters.AddWithValue("@NewDatabaseName", @event.ConnectionPoolUpdate.DatabaseName);
                            command.Parameters.AddWithValue("@TenantId", @event.ConnectionPoolUpdate.TenantId);

                            try
                            {
                                if (await command.ExecuteNonQueryAsync() > 0)
                                {
                                    updateCompany = true;
                                    _logger.LogInformation("{DateTime} : Company database name updated.", DateTime.UtcNow);
                                }
                                else
                                {
                                    updateCompany = false;
                                    _logger.LogError("{DateTime} : Company database name not updated !", DateTime.UtcNow);
                                }
                            }
                            catch (NpgsqlException ex)
                            {
                                _logger.LogError("{DateTime} : Error updating Company database name: {Error}", DateTime.UtcNow, ex.Message);
                                updateCompany = false;
                            }
                        }
                        await connection.CloseAsync();
                        await connection.DisposeAsync();
                    }
                    if (updateConPool is true && updateCompany is true)
                    {
                        await _context.Set<MenuDatabase>().AddAsync(menuDatabase);
                        await _context.SaveChangesAsync();
                    }
                    else
                        _logger.LogError("{DateTime} : MenuDatabase value is didn't added in database !", DateTime.UtcNow);

                    await _redisStreamService.PublishEventAsync(new DatabaseCreatedProcessStreamEvent(@event.ConnectionPoolUpdate.TenantId, true), StreamEnum.AuthJob);
                }
                else
                {
                    //await _redisStreamService.PublishEventAsync(new DatabaseCreatedProcessStreamEvent(@event.ConnectionPoolUpdate.TenantId, true), StreamEnum.AuthJob);

                    _logger.LogCritical("{DateTime} : Database already exists !", DateTime.UtcNow);
                }
            }
            else
            {
                _logger.LogError("{DateTime} : {Datebase} already exists. You are try the other database name", DateTime.UtcNow, @event.ConnectionPoolUpdate.DatabaseName);
            }

            #region
            List<ConnectionPoolWithCompanyModel>? withCompanyModels = await _cacheManager.GetAsync<List<ConnectionPoolWithCompanyModel>>(CacheKey.Create("connectionwithcompany"), () => new List<ConnectionPoolWithCompanyModel>());

            await _redisStreamService.PublishEventAsync(new DatabaseNotificationStreamEvent(withCompanyModels.Select(cm => cm.DbUrls).ToList()), StreamEnum.TenantApi);
            #endregion
        }
    }
}