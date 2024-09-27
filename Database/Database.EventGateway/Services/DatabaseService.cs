using Npgsql;
using System.Data.SqlClient;

namespace Database.EventGateway.Services
{
    public class DatabaseService : IDatabaseService
    {
        private string _connectionString;
        private string dfaultDbName;
        private readonly ILogger<DatabaseService> _logger;
        public DatabaseService(ILogger<DatabaseService> logger = null)
        {
            dfaultDbName = "postgres";
            _logger = logger;
        }

        public async Task SetConnectionStringAsync(string connectionString)
        {
            await Task.Run(() =>
            {
                _connectionString = connectionString;
            });
        }

        public async Task<bool> CreateDatabaseIfNotExistsAsync(string dbName)
        {
            var checkDbCommand = @"
                    SELECT 1 FROM pg_database WHERE datname = @DbName;
                ";

            var createDbCommand = @"
                    CREATE DATABASE " + dbName + @";
                ";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var checkCommand = new NpgsqlCommand(checkDbCommand, connection))
                {
                    checkCommand.Parameters.AddWithValue("@DbName", dbName);
                    var exists = await checkCommand.ExecuteScalarAsync();

                    if (exists == null)
                        using (var createCommand = new NpgsqlCommand(createDbCommand, connection))
                        {
                            await createCommand.ExecuteNonQueryAsync();
                            return true;
                        }
                    else
                        return false;
                }
            }
        }
        public async Task<bool> DropDatabaseIfExistsAsync(string dbName)
        {
            var dropDbCommand = @"
                DROP DATABASE ""{dbName}"";";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var checkCommand = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @DbName;", connection))
                {
                    checkCommand.Parameters.AddWithValue("@DbName", dbName);
                    var exists = await checkCommand.ExecuteScalarAsync();

                    if (exists != null)
                    {
                        using (var dropConnection = new NpgsqlConnection(_connectionString))
                        {
                            await dropConnection.OpenAsync();
                            using (var dropCommand = new NpgsqlCommand(dropDbCommand, dropConnection))
                            {
                                await dropCommand.ExecuteNonQueryAsync();
                                return true;
                            }
                        }
                    }
                    else
                        return false;
                }
            }
        }
    }
}
