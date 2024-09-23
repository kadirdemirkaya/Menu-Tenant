using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.MenuAggregate.ValueObjects
{
    public sealed class MenuId : ValueObject
    {
        public Guid Id { get; }

        public MenuId()
        {
            Id = Guid.NewGuid();
        }
        public MenuId(Guid id)
        {
            Id = id;
        }

        public static MenuId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static MenuId Create(Guid Id)
        {
            return new MenuId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
