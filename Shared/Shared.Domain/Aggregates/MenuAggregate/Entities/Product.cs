using Shared.Domain.Aggregates.MenuAggregate.Enums;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.MenuAggregate.Entities
{
    public class Product : Entity<ProductId>
    {
        public string Title { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductStatus ProductStatus { get; private set; }

        public ProductDetail ProductDetails { get; set; }


        public MenuId MenuId { get; set; }
        public Menu Menu { get; set; }

        public Product()
        {
            
        }

        public Product(ProductId id) : base(id)
        {
            Id = id;
        }
    }
}
