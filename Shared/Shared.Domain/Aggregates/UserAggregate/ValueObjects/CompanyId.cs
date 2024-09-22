using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.UserAggregate.ValueObjects
{
    public sealed class CompanyId : ValueObject
    {
        public Guid Id { get; }


        public CompanyId(Guid id)
        {
            Id = id;
        }

        public static CompanyId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static CompanyId Create(Guid Id)
        {
            return new CompanyId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
