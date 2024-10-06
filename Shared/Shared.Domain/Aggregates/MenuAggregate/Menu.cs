using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.ProductAggregate
{
    public class Menu : AggregateRoot<MenuId>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? WebUrl { get; private set; } // name for route 
        public bool IsActive { get; set; } = false;

        public Address Address { get; private set; }


        public readonly List<Product> Products = new();
        public IReadOnlyCollection<Product> _products => Products.AsReadOnly();

        public Menu()
        {

        }

        public Menu(MenuId id) : base(id)
        {
            Id = id;
        }

        public Menu(string name, string? description, string? webUrl)
        {
            Id = MenuId.CreateUnique();
            Name = name;
            Description = description;
            WebUrl = webUrl;
        }
        public Menu(string name, string tenantId, string? description, string? webUrl)
        {
            Id = MenuId.CreateUnique();
            TenantId = tenantId;
            Name = name;
            Description = description;
            WebUrl = webUrl;
        }

        public Menu(MenuId menuId, string name, string? description, string? webUrl)
        {
            Id = menuId;
            Name = name;
            Description = description;
            WebUrl = webUrl;
        }

        public Menu(MenuId id, string name, string tenantId, string? description, string? webUrl) : base(id)
        {
            Name = name;
            Description = description;
            WebUrl = webUrl;
        }

        public static Menu Create(string name, string? description, string? webUrl)
            => new(name, description, webUrl);

        public static Menu Create(MenuId menuId, string name, string? description, string? webUrl)
         => new(menuId, name, description, webUrl);

        public static Menu Create(string name, string tenantId, string? description, string? webUrl)
            => new(name, description, webUrl);

        public static Menu Create(MenuId id, string name, string tenantId, string? description, string? webUrl)
            => new(id, name, tenantId, description, webUrl);

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetWebUrl(string webUrl)
        {
            WebUrl = webUrl;
        }

        public void SetActive(bool activeState)
        {
            IsActive = activeState;
        }

        public void UpdateMenu(string name, string? description, string? webUrl)
        {
            SetName(name);
            SetDescription(description ?? string.Empty);
            SetWebUrl(webUrl ?? string.Empty);
        }

        public void UpdateMenu(MenuId id, string name, string? description, string? webUrl)
        {
            Id = id;
            SetName(name);
            SetDescription(description ?? string.Empty);
            SetWebUrl(webUrl ?? string.Empty);
        }
        public void UpdateMenu(MenuId id, string name, string? description, string? webUrl, string tenantId)
        {
            Id = id;
            SetName(name);
            SetDescription(description ?? string.Empty);
            SetWebUrl(webUrl ?? string.Empty);
            SetTenantId(tenantId);
        }

        public void AddAddress(Address address)
        {
            Address = address;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
