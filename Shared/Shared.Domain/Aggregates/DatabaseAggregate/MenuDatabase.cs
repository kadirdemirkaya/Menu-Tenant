using Shared.Domain.Aggregates.DatabaseAggregate.ValueObjects;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.MenuDatabaseAggregate
{
    public class MenuDatabase : AggregateRoot<MenuDatabaseId>
    {
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string DatabaseName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        public MenuDatabase()
        {

        }

        public MenuDatabase(MenuDatabaseId id) : base(id)
        {

        }

        public MenuDatabase(string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
        {
            Id = MenuDatabaseId.CreateUnique();
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
            TenantId = tenantid;
            IsActive = isActive;
        }

        public MenuDatabase(MenuDatabaseId MenuDatabaseId, string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true) : base(MenuDatabaseId)
        {
            Id = MenuDatabaseId;
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
            TenantId = tenantid;
            IsActive = isActive;
        }

        public static MenuDatabase Create(string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
          => new(host, port, databaseName, userName, password, tenantid, isActive);

        public static MenuDatabase Create(MenuDatabaseId MenuDatabaseId, string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
          => new(MenuDatabaseId, host, port, databaseName, userName, password, tenantid, isActive);

        public void SetMenuDatabaseName(string databaseName)
        {
            DatabaseName = databaseName;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
