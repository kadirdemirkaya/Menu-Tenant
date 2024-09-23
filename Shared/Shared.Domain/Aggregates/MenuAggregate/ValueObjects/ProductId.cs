using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.MenuAggregate.ValueObjects
{
    public sealed class ProductId : ValueObject
    {
        public Guid Id { get; }

        public ProductId()
        {
            Id = Guid.NewGuid();
        }
        public ProductId(Guid id)
        {
            Id = id;
        }

        public static ProductId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static ProductId Create(Guid Id)
        {
            return new ProductId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
