using Microsoft.AspNetCore.Http;

namespace Shared.Domain.Models.Product
{
    public class MenuProductsModel
    {
        public Guid MenuId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public string? Description { get; set; } // content
        public double? WeightInGrams { get; set; } // gr bv.
    }
}
