using Shared.Domain.Aggregates.MenuAggregate.Enums;

namespace Tenant.Application.Dtos
{
    public class AllProductModelDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public string? Description { get; set; } // content
        public double? WeightInGrams { get; set; } // gr bv.

        public AllProductModelDto()
        {
            
        }

        public AllProductModelDto ProductMapper(Guid id, string title, string name, decimal price, byte[] image, string? description, double? weightInGrams)
        {
            Id = id;
            Title = title;
            Name = name;
            Price = price;
            Image = image;
            Description = description;
            WeightInGrams = weightInGrams;

            return this;
        }
    }
}
