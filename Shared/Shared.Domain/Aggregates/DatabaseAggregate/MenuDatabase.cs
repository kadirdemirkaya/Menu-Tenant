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

        public static MenuDatabase Create(MenuDatabaseId menuDatabaseId, string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
          => new(menuDatabaseId, host, port, databaseName, userName, password, tenantid, isActive);

        public void UpdateMenuDatabase(string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
        {
            SetHost(host);
            SetPort(port);
            SetDatabaseName(databaseName);
            SetUsername(userName);
            SetPassword(password);
            SetTenantId(tenantid);
            SetIsActive(isActive);
        }
        public void UpdateMenuDatabase(MenuDatabaseId menuDatabaseId, string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
        {
            Id = menuDatabaseId;
            SetHost(host);
            SetPort(port);
            SetDatabaseName(databaseName);
            SetUsername(userName);
            SetPassword(password);
            SetTenantId(tenantid);
            SetIsActive(isActive);
        }

        public void SetHost(string host)
        {
            Host = host;
        }

        public void SetPort(string port)
        {
            Port = port;
        }

        public void SetDatabaseName(string databaseName)
        {
            DatabaseName = databaseName;
        }

        public void SetUsername(string userName)
        {
            Username = userName;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
