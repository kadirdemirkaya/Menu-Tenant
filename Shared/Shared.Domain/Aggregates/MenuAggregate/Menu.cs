using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.ProductAggregate
{
    public class Menu : AggregateRoot<MenuId>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? WebUrl { get; private set; }

        public Address Address { get; private set; }

        public List<Product> Products { get; private set; }

    }
}
