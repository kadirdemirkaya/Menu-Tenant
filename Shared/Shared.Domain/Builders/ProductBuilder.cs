using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.Enums;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Builders
{
    public class ProductBuilder
    {
        private ProductId _productId;
        private string _title;
        private string _name;
        private decimal _price;
        private byte[] _image;
        private ProductDetail _productDetail;
        private MenuId _menuId;
        private string _tenantId;
        private ProductStatus _productStatus = ProductStatus.InStock;

        public ProductBuilder SetId(ProductId id) { _productId = id; return this; }
        public ProductBuilder SetTitle(string title) { _title = title; return this; }
        public ProductBuilder SetName(string name) { _name = name; return this; }
        public ProductBuilder SetPrice(decimal price) { _price = price; return this; }
        public ProductBuilder SetImage(byte[] image) { _image = image; return this; }
        public ProductBuilder SetProductDetail(ProductDetail productDetail) { _productDetail = productDetail; return this; }
        public ProductBuilder SetMenuId(MenuId menuId) { _menuId = menuId; return this; }
        public ProductBuilder SetTenantId(string tenantId) { _tenantId = tenantId; return this; }
        public ProductBuilder SetProductStatus(ProductStatus productStatus) { _productStatus = productStatus; return this; }

        public Product Build(ConstructorType constructorType)
        {
            if (constructorType == ConstructorType.Withd)
            {
                Product product = new(_productId, _title, _name, _price, _image, _productDetail, _menuId, _tenantId, _productStatus);
                product.AddProductDetail(_productDetail);
                return product;
            }
            else if (constructorType == ConstructorType.NoId)
            {
                Product product = new(_title, _name, _price, _image, _productDetail, _menuId, _tenantId, _productStatus);
                product.AddProductDetail(_productDetail);
                return product;
            }
            else
            {
                Product product = new(_title, _name, _price, _image, _productDetail, _menuId, _productStatus);
                product.AddProductDetail(_productDetail);
                return product;
            }

        }
    }
}
