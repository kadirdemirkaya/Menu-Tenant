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
    }
}
