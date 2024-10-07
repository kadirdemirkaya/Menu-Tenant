using Shared.Domain.Aggregates.DatabaseAggregate.ValueObjects;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
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


        public MenuDatabase SetId(MenuDatabaseId id) { Id = id; return this; }
        public MenuDatabase SetHost(string host) { Host = host; return this; }
        public MenuDatabase SetPort(string port) { Port = port; return this; }
        public MenuDatabase SetDatabaseName(string databaseName) { DatabaseName = databaseName; return this; }
        public MenuDatabase SetUsername(string userName) { Username = userName; return this; }
        public MenuDatabase SetPassword(string password) { Password = password; return this; }
        public MenuDatabase SetIsActive(bool isActive ) { IsActive = isActive; return this; }
        public MenuDatabase SetTenantIdForEntity(string id) { SetTenantId(id); return this; }
        public MenuDatabase SetUpdatedDate(DateTime updateDate) { SetCreatedDateUTC(updateDate); return this; }
        public MenuDatabase SetCreatedDate(DateTime createdDate) { SetCreatedDateUTC(createdDate); return this; }
        public MenuDatabase SetIsDeletedForEntity(bool isDeleted) { SetIsDeleted(isDeleted); return this; }


        #region Old Setter Method
        //public void SetHost(string host)
        //{
        //    Host = host;
        //}

        //public void SetPort(string port)
        //{
        //    Port = port;
        //}

        //public void SetDatabaseName(string databaseName)
        //{
        //    DatabaseName = databaseName;
        //}

        //public void SetUsername(string userName)
        //{
        //    Username = userName;
        //}

        //public void SetPassword(string password)
        //{
        //    Password = password;
        //}

        //public void SetIsActive(bool isActive)
        //{
        //    IsActive = isActive;
        //}
        #endregion

        public void UpdateMenuDatabase(string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
        {
            SetHost(host)
            .SetPort(port)
            .SetDatabaseName(databaseName)
            .SetUsername(userName)
            .SetPassword(password)
            .SetTenantIdForEntity(tenantid)
            .SetIsActive(isActive)
            .SetUpdatedDate(DateTime.UtcNow);
        }
        public void UpdateMenuDatabase(MenuDatabaseId menuDatabaseId, string host, string port, string databaseName, string userName, string password, string tenantid, bool isActive = true)
        {
            SetId(menuDatabaseId)
            .SetHost(host)
            .SetPort(port)
            .SetDatabaseName(databaseName)
            .SetUsername(userName)
            .SetPassword(password)
            .SetTenantIdForEntity(tenantid)
            .SetIsActive(isActive)
            .SetUpdatedDate(DateTime.UtcNow);
        }
    }
}
