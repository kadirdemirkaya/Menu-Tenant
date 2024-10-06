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

        public Company(CompanyId companyId, string name, string databaseName, AppUserId appUserId, string tenantId) : base(companyId)
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


        public Company SetId(CompanyId id) { Id = id; return this; }
        public Company SetName(string name) { Name = name; return this; }
        public Company SetDatabaseName(string databaseName) { DatabaseName = databaseName; return this; }
        public Company SetAppUserId(AppUserId appUserId) { AppUserId = appUserId; return this; }
        public Company SetConnectionPool(ConnectionPool connectionPool) { ConnectionPool = connectionPool; return this; }
        public Company SetTenantIdForEntity(string id) { SetTenantId(id); return this; }
        public Company SetUpdatedDate(DateTime updateDate) { SetCreatedDateUTC(updateDate); return this; }
        public Company SetCreatedDate(DateTime createdDate) { SetCreatedDateUTC(createdDate); return this; }
        public Company SetIsDeletedForEntity(bool isDeleted) { SetIsDeleted(isDeleted); return this; }


        #region Old Setter Method
        //public void SetName(string name)
        //{
        //    Name = name;
        //}

        //public void SetDatabaseName(string databaseName)
        //{
        //    DatabaseName = databaseName;
        //}

        //public void SetAppUserId(AppUserId appUserId)
        //{
        //    AppUserId = appUserId;
        //}

        //public void SetTenantIdForEntity(string id)
        //{
        //    SetTenantId(id);
        //}
        #endregion

        public void UpdateCompany(string name, string databaseName, AppUserId appUserId)
        {
            SetName(name)
            .SetDatabaseName(databaseName)
            .SetAppUserId(appUserId);
        }
        public void UpdateCompany(CompanyId companyId, string name, string databaseName, AppUserId appUserId)
        {
            SetId(companyId)
            .SetName(name)
            .SetDatabaseName(databaseName)
            .SetAppUserId(appUserId);
        }
        public void UpdateCompany(CompanyId companyId, string name, string databaseName, AppUserId appUserId, string tenantId)
        {
            SetId(companyId)
            .SetName(name)
            .SetDatabaseName(databaseName)
            .SetAppUserId(appUserId)
            .SetTenantId(tenantId);
        }
    }
}
