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
        public byte[] Image { get; private set; }

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

        public Product(string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId, ProductStatus productStatus = ProductStatus.InStock)
        {
            Id = ProductId.CreateUnique();
            Title = title;
            Name = name;
            ProductDetails = productDetail;
            MenuId = menuId;
            ProductStatus = productStatus;
            Image = image;
        }

        public Product(ProductId productId, string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId, ProductStatus productStatus = ProductStatus.InStock) : base(productId)
        {
            Id = productId;
            Title = title;
            Name = name;
            ProductDetails = productDetail;
            MenuId = menuId;
            ProductStatus = productStatus;
            Image = image;
        }

        public static Product Create(string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId, ProductStatus productStatus = ProductStatus.InStock)
            => new(title, name, price, image, productDetail, menuId, productStatus);

        public static Product Create(ProductId productId, string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId, ProductStatus productStatus = ProductStatus.InStock)
           => new(productId, title, name, price, image, productDetail, menuId, productStatus);

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public void SetProductStatus(ProductStatus productStatus)
        {
            ProductStatus = productStatus;
        }

        public void SetMenuId(MenuId menuId)
        {
            MenuId = menuId;
        }

        public void SetImage(byte[] image)
        {
            Image = image;
        }

        public void AddProductDetail(ProductDetail productDetail)
        {
            ProductDetails = productDetail;
        }


        public void UpdateProduct(string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId, ProductStatus productStatus = ProductStatus.InStock)
        {
            SetTitle(title);
            SetName(name);
            SetPrice(price);
            SetImage(image);
            AddProductDetail(productDetail);
            SetMenuId(menuId);
            SetProductStatus(productStatus);
        }

        public void UpdateProduct(ProductId productId, string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId, ProductStatus productStatus = ProductStatus.InStock)
        {
            Id = productId;
            SetTitle(title);
            SetName(name);
            SetPrice(price);
            SetImage(image);
            AddProductDetail(productDetail);
            SetMenuId(menuId);
            SetProductStatus(productStatus);
        }

        public void UpdateProduct(ProductId productId, string title, string name, decimal price, byte[] image, ProductDetail productDetail, MenuId menuId,string tenantId, ProductStatus productStatus = ProductStatus.InStock)
        {
            Id = productId;
            SetTitle(title);
            SetName(name);
            SetPrice(price);
            SetImage(image);
            AddProductDetail(productDetail);
            SetMenuId(menuId);
            SetProductStatus(productStatus);
            SetTenantId(tenantId);
        }
    }
}
