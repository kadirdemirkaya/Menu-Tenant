using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.UserAggregate
{
    public class AppUser : AggregateRoot<AppUserId>
    {
        public string? Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }

        public List<Company> Companies { get; private set; }
    }
}
