using Microsoft.AspNetCore.Http;

namespace Tenant.Application.Dtos
{
    public class UpdateProductDto
    {
        public Guid ProductId { get; set; }
        public Guid MenuId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public string? Description { get; set; } // content
        public double? WeightInGrams { get; set; } // gr bv.
    }
}
