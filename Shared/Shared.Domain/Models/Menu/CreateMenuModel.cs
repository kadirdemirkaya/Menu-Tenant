using Shared.Domain.Models.Product;

namespace Shared.Domain.Models.Menu
{
    public class CreateMenuModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
