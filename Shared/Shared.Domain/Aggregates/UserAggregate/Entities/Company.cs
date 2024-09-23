using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.BaseTypes;
using System.Security.Principal;

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
    }
}
