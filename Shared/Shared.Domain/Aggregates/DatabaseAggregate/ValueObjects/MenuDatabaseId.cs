using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.DatabaseAggregate.ValueObjects
{
    public sealed class MenuDatabaseId : ValueObject
    {
        public Guid Id { get; }

        public MenuDatabaseId()
        {
            Id = Guid.NewGuid();
        }
        public MenuDatabaseId(Guid id)
        {
            Id = id;
        }

        public static MenuDatabaseId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static MenuDatabaseId Create(Guid Id)
        {
            return new MenuDatabaseId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
