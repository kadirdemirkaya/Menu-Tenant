using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.BaseTypes;

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

        public Company(string name, string databaseName, AppUserId appUserId)
        {
            Id = CompanyId.CreateUnique();
            Name = name;
            DatabaseName = databaseName;
            AppUserId = appUserId;
        }

        public Company(CompanyId companyId, string name, string databaseName, AppUserId appUserId) : base(companyId)
        {
            Id = companyId;
            Name = name;
            DatabaseName = databaseName;
            AppUserId = appUserId;
        }

        public Company(CompanyId companyId, string name, string databaseName, AppUserId appUserId,string tenantId) : base(companyId)
        {
            Id = companyId;
            Name = name;
            DatabaseName = databaseName;
            AppUserId = appUserId;
            SetTenantIdForEntity(tenantId);
        }

        public static Company Create(string name, string databaseName, AppUserId appUserId)
            => new(name, databaseName, appUserId);

        public static Company Create(CompanyId companyId, string name, string databaseName, AppUserId appUserId)
          => new(companyId, name, databaseName, appUserId);

        public static Company Create(CompanyId companyId, string name, string databaseName, AppUserId appUserId, string tenantId)
        => new(companyId, name, databaseName, appUserId, tenantId);

        public void AddConnectionPool(ConnectionPool connectionPool)
        {
            ConnectionPool = connectionPool;
        }

        public void RemoveConnectionPool()
        {
            if (ConnectionPool != null)
                ConnectionPool = null;
        }

        public void AppUserIdSet(AppUserId appUserId)
        {
            AppUserId = appUserId;
        }

        public void SetTenantIdForEntity(string id)
        {
            SetTenantId(id);
        }
    }
}
