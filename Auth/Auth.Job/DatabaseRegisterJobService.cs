using Auth.Infrastructure.Data;
using Auth.Job.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models.ConnectionPools;
using Shared.Infrastructure.Extensions;
using Shared.Stream;

namespace Auth.Job
{
    public class DatabaseRegisterJobService : IJob
    {
        private readonly AuthDbContext _context;
        private IRepository<ConnectionPool, ConnectionPoolId> _repository;
        private ISecretsManagerService _secretsManagerService;
        private ILogger<DatabaseRegisterJobService> _logger;
        private StreamBus _streamBus;
        private RedisStreamService _redisStreamService;
        public DatabaseRegisterJobService(IRepository<ConnectionPool, ConnectionPoolId> repository, ISecretsManagerService secretsManagerService, ILogger<DatabaseRegisterJobService> logger, StreamBus streamBus, AuthDbContext context, RedisStreamService redisStreamService)
        {
            _repository = repository;
            _secretsManagerService = secretsManagerService;
            _logger = logger;
            _streamBus = streamBus;
            _context = context;
            _redisStreamService = redisStreamService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                List<ConnectionPool>? connectionPools = await _context.Set<ConnectionPool>().IgnoreQueryFilters().AsNoTracking().Where(c => c.IsActive == false && c.DatabaseName.StartsWith("personaldb_")).Include(c => c.Company).ToListAsync();

                #region got error then will fixed
                //List<ConnectionPool>? connectionPools = await _repository.GetAllAsync(c => c.IsActive == false || c.DatabaseName.StartsWith("personaldb_"), false, true, c => c.Company, com => com.Company.AppUser);
                #endregion

                if (connectionPools.Count() != 0)
                {
                    foreach (var con in connectionPools)
                    {
                        if (con.IsActive == false && con.DatabaseName.StartsWith("personaldb_"))
                        {
                            string personalDbName = $"{con.Company.Name}{StringExtension.GenerateRandomDbName()}db";

                            con.SetDatabaseName(personalDbName);
                            con.SetUpdatedDateUTC(DateTime.UtcNow);

                            bool isUpdate = false;

                            isUpdate = _repository.Update(con);
                            isUpdate = await _repository.SaveCahangesAsync();

                            if (!isUpdate)
                                _logger.LogError("got error in system while table udpate");

                            if (isUpdate)
                            {
                                ConnectionPoolUpdateModel connectionPoolUpdate = new()
                                {
                                    DatabaseName = con.DatabaseName,
                                    Host = con.Host,
                                    Name = con.Name,
                                    Password = con.Password,
                                    Port = con.Port,
                                    TenantId = con.TenantId,
                                    Username = con.Username
                                };

                                await _redisStreamService.PublishEventAsync(new ConnectionPoolUpdateStreamEvent(connectionPoolUpdate), StreamEnum.DatabaseEventGateway);
                                _logger.LogInformation("{DateTime} : event is sended in job servis", DateTime.UtcNow);
                            }
                        }
                        else
                        {
                            string personalDbName = con.DatabaseName;

                            con.SetUpdatedDateUTC(DateTime.UtcNow);

                            ConnectionPoolUpdateModel connectionPoolUpdate = new()
                            {
                                DatabaseName = con.DatabaseName,
                                Host = con.Host,
                                Name = con.Name,
                                Password = con.Password,
                                Port = con.Port,
                                TenantId = con.TenantId,
                                Username = con.Username
                            };

                            await _redisStreamService.PublishEventAsync(new ConnectionPoolUpdateStreamEvent(connectionPoolUpdate), StreamEnum.DatabaseEventGateway);
                            _logger.LogInformation("{DateTime} : event is sended in job servis", DateTime.UtcNow);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{DateTime} : Job servis is got error !", DateTime.UtcNow);
            }
        }
    }
}
