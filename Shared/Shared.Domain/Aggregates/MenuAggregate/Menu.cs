using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.ProductAggregate
{
    public class Menu : AggregateRoot<MenuId>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? WebUrl { get; private set; }

        public Address Address { get; private set; }


        public readonly List<Product> Products = new();
        public IReadOnlyCollection<Product> _products => Products.AsReadOnly();

        public Menu()
        {
            
        }

        public Menu(MenuId id) : base(id)
        {
            Id = id;
        }

    }
}
