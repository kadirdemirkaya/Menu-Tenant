using Microsoft.Extensions.Logging;
using Quartz;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Infrastructure.Extensions;

namespace Auth.Job
{
    public class DatabaseRegisterJobService(IRepository<ConnectionPool, ConnectionPoolId> _repository, ISecretsManagerService secretsManagerService, ILogger<DatabaseRegisterJobService> _logger) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                List<ConnectionPool>? connectionPools = await _repository.GetAllAsync(c => c.IsActive == false || c.DatabaseName.StartsWith("personaldb_"), false, true, c => c.Company, com => com.Company.AppUser);

                if (connectionPools.Count() != 0)
                {
                    connectionPools.ForEach(async con =>
                    {
                        if (con.IsActive == false && con.DatabaseName.StartsWith("personaldb_"))
                        {
                            string personalDbName = $"{con.Company.AppUser.Username}{StringExtension.GenerateTemporaryDatabaseName()}db";

                            con.SetDatabaseName(personalDbName);
                            con.SetUpdatedDateUTC(DateTime.UtcNow);

                            bool isUpdate = false;

                            isUpdate = _repository.Update(con);
                            isUpdate = await _repository.SaveCahangesAsync();

                            if (isUpdate)
                                _logger.LogError("got error in system while table udpate");

                            if (isUpdate)
                            {
                                //
                                // in this section will send event for db create process
                                //
                            }

                        }
                        else
                        {
                            string personalDbName = con.DatabaseName;

                            con.SetUpdatedDateUTC(DateTime.UtcNow);

                            //
                            // in this section will send event for db create process
                            //
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
