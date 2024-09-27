
namespace Database.EventGateway.Services
{
    public interface IDatabaseService
    {
        Task SetConnectionStringAsync(string connectionString);
        Task<bool> CreateDatabaseIfNotExistsAsync(string dbName);
        Task<bool> DropDatabaseIfExistsAsync(string dbName);
    }
}