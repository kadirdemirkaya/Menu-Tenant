using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenant.Application.Dtos
{
    public class AddProductsModelDto
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public string? Description { get; set; } // content
        public double? WeightInGrams { get; set; } // gr bv.
    }
}
