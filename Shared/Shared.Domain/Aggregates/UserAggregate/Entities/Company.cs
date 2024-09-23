using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.BaseTypes;
using System.Security.Principal;
using System.Xml.Linq;

namespace Shared.Domain.Aggregates.UserAggregate.Entities
{
    public class Company : Entity<CompanyId>
    {
        public string Name { get; private set; }
        public string DatabaseName { get; private set; }

        public AppUserId AppUserId { get; private set; }
        public AppUser AppUser { get; private set; }

        public ConnectionPool ConnectionPool { get; private set; }

        public Company()
        {

        }

        public Company(CompanyId id) : base(id)
        {
            Id = id;
        }

        public Company(string name, string databaseName, AppUserId appUserId, ConnectionPool connectionPool)
        {
            Id = CompanyId.CreateUnique();
            Name = name;
            DatabaseName = databaseName;
            AppUserId = appUserId;
        }

        public Company(CompanyId companyId, string name, string databaseName, AppUserId appUserId, ConnectionPool connectionPool) : base(companyId)
        {
            Id = companyId;
            Name = name;
            DatabaseName = databaseName;
            AppUserId = appUserId;
        }

        public static Company Create(string name, string databaseName, AppUserId appUserId, ConnectionPool connectionPool)
            => new(name, databaseName, appUserId, connectionPool);

        public static Company Create(CompanyId companyId, string name, string databaseName, AppUserId appUserId, ConnectionPool connectionPool)
          => new(companyId, name, databaseName, appUserId, connectionPool);
    }
}
