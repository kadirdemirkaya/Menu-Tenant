using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.UserAggregate.ValueObjects
{
    public sealed class AppUserId : ValueObject
    {
        public Guid Id { get; }

        public AppUserId()
        {
            Id = Guid.NewGuid();
        }
        public AppUserId(Guid id)
        {
            Id = id;
        }

        public static AppUserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static AppUserId Create(Guid Id)
        {
            return new AppUserId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
