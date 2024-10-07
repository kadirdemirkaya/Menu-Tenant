using Microsoft.Extensions.Primitives;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.BaseTypes;
using System.Security.Principal;

namespace Shared.Domain.Aggregates.UserAggregate.Entities
{
    public class ConnectionPool : Entity<ConnectionPoolId>
    {
        public string Name { get; private set; }
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string DatabaseName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        public CompanyId CompanyId { get; private set; }
        public Company Company { get; private set; }

        public ConnectionPool()
        {

        }

        public ConnectionPool(ConnectionPoolId id) : base(id)
        {
            Id = id;
        }

        public ConnectionPool(string name, string host, string port, string databaseName, string userName, string password, CompanyId companyId, bool isActive = true)
        {
            Id = ConnectionPoolId.CreateUnique();
            Name = name;
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
            CompanyId = companyId;
            IsActive = isActive;
        }

        public ConnectionPool(ConnectionPoolId connectionPoolId, string name, string host, string port, string databaseName, string userName, string password, CompanyId companyId, bool isActive = true)
        {
            Id = connectionPoolId;
            Name = name;
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
            CompanyId = companyId;
            IsActive = isActive;
        }

        public ConnectionPool(ConnectionPoolId connectionPoolId, string name, string host, string port, string databaseName, string userName, string password, CompanyId companyId, string tenantId, bool isActive = true)
        {
            Id = connectionPoolId;
            Name = name;
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
            CompanyId = companyId;
            IsActive = isActive;
            SetTenantIdForEntity(tenantId);
        }

        public static ConnectionPool Create(string name, string host, string port, string databaseName, string userName, string password, CompanyId companyId, bool isActive = true)
            => new(name, host, port, databaseName, userName, password, companyId, isActive);

        public static ConnectionPool Create(ConnectionPoolId connectionPoolId, string name, string host, string port, string databaseName, string userName, string password, CompanyId companyId, bool isActive = true)
            => new(connectionPoolId, name, host, port, databaseName, userName, password, companyId, isActive);

        public static ConnectionPool Create(ConnectionPoolId connectionPoolId, string name, string host, string port, string databaseName, string userName, string password, CompanyId companyId, string tenantId, bool isActive = true)
            => new(connectionPoolId, name, host, port, databaseName, userName, password, companyId, tenantId, isActive);


        public ConnectionPool SetId(ConnectionPoolId id) { Id = id; return this; }
        public ConnectionPool SetName(string name) { Name = name; return this; }
        public ConnectionPool SetHost(string host) { Host = host; return this; }
        public ConnectionPool SetPort(string port) { Port = port; return this; }
        public ConnectionPool SetDatabaseName(string databaseName) { DatabaseName = databaseName; return this; }
        public ConnectionPool SetUsername(string username) { Username = username; return this; }
        public ConnectionPool SetPassword(string password) { Password = password; return this; }
        public ConnectionPool SetIsActive(bool isActive) { IsActive = IsActive; return this; }
        public ConnectionPool CompanyIdSet(CompanyId companyId) { CompanyId = companyId; return this; }
        public ConnectionPool SetTenantIdForEntity(string id) { SetTenantId(id); return this; }
        public ConnectionPool SetUpdatedDate(DateTime updateDate) { SetCreatedDateUTC(updateDate); return this; }
        public ConnectionPool SetCreatedDate(DateTime createdDate) { SetCreatedDateUTC(createdDate); return this; }
        public ConnectionPool SetIsDeletedForEntity(bool isDeleted) { SetIsDeleted(isDeleted); return this; }


        #region Old Setter Methods
        //public void SetDatabaseName(string databaseName)
        //{
        //    DatabaseName = databaseName;
        //}

        //public void SetIsActive(bool isActive)
        //{
        //    IsActive = isActive;
        //}

        //public void CompanyIdSet(CompanyId companyId)
        //{
        //    CompanyId = companyId;
        //}

        //public void SetTenantIdForEntity(string id)
        //{
        //    SetTenantId(id);
        //}

        #endregion
    }
}
