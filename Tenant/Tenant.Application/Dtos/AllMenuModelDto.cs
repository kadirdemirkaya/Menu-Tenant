using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;

namespace Tenant.Application.Dtos
{
    public class AllMenuModelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? WebUrl { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }

        public List<AllProductModelDto> AllProductModelDtos { get; set; } = new();

        public AllMenuModelDto MenuMapper(Guid id, string name, bool isactive, Address address, List<Product>? products, string? description = "", string? webUrl = "")
        {
            Id = id;
            Name = name;
            Description = description;
            WebUrl = webUrl;
            Street = address.Street;
            City = address.City;
            Country = address.Country;
            IsActive = isactive;

            if (products is not null && products.Any())
                foreach (var product in products)
                {
                    AllProductModelDto allProductModelDto = new();

                    AllProductModelDtos.Add(allProductModelDto.ProductMapper(product.Id.Id, product.Title, product.Name, product.Price, product.Image, product.ProductDetails.Description, product.ProductDetails.WeightInGrams));
                }

            return this;
        }
    }
}
