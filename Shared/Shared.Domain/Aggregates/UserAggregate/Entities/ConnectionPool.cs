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

        public CompanyId CompanyId { get; private set; }
        public Company Company { get; private set; }

        public ConnectionPool()
        {

        }

        public ConnectionPool(ConnectionPoolId id) : base(id)
        {
            Id = id;
        }

        public ConnectionPool(string name, string host, string port, string databaseName, string userName, string password)
        {
            Id = ConnectionPoolId.CreateUnique();
            Name = name;
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
        }

        public ConnectionPool(ConnectionPoolId connectionPoolId, string name, string host, string port, string databaseName, string userName, string password)
        {
            Id = connectionPoolId;
            Name = name;
            Host = host;
            Port = port;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
        }

        public static ConnectionPool Create(string name, string host, string port, string databaseName, string userName, string password)
            => new(name, host, port, databaseName, userName, password);

        public static ConnectionPool Create(ConnectionPoolId connectionPoolId, string name, string host, string port, string databaseName, string userName, string password)
            => new(connectionPoolId, name, host, port, databaseName, userName, password);
    }
}
