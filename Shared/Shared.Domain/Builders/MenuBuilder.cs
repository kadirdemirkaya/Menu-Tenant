using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Builders
{
    public class MenuBuilder
    {
        private MenuId _id;
        private string _name;
        private string? _description;
        private string? _weburl;
        private bool _isactive = false;
        private string _tenantId;
        private Address _address;
        private List<Product> _products;

        public MenuBuilder SetId(MenuId menuId) { _id = menuId; return this; }
        public MenuBuilder SetName(string name) { _name = name; return this; }

        public MenuBuilder SetDescription(string descriptino) { _description = descriptino; return this; }

        public MenuBuilder SetWeburl(string webUrl) { _weburl = webUrl; return this; }

        public MenuBuilder SetIsActive(bool isactive) { _isactive = isactive; return this; }

        public MenuBuilder SetTenantId(string tenantId) { _tenantId = tenantId; return this; }

        public MenuBuilder SetAddress(Address address) { _address = address; return this; }

        public MenuBuilder AddAddress(Product product)
        {
            _products.Add(product);
            return this;
        }

        public Menu Build(ConstructorType constructorType)
        {
            if (constructorType == ConstructorType.Withd)
            {
                Menu menu = new(_id, _name, _tenantId, _description, _weburl);
                menu.AddAddress(_address);
                foreach (var product in _products)
                    menu.AddProduct(product);
                return menu;
            }
            else if (constructorType == ConstructorType.NoId)
            {
                Menu menu = new(_name, _tenantId, _description, _weburl);
                menu.AddAddress(_address);
                foreach (var product in _products)
                    menu.AddProduct(product);
                return menu;
            }
            else
            {
                Menu menu = new(_name, _description, _weburl);
                menu.AddAddress(_address);
                foreach (var product in _products)
                    menu.AddProduct(product);
                return menu;
            }
        }
    }
}
