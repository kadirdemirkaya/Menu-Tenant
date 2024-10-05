using EventBusDomain;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Commands.Requests
{
    public class CreateProductCommandRequest : IEventRequest
    {
        public MenuId MenuId { get; set; }
        public List<AddProductsModelDto> AddProductsModelDtos { get; set; }
        
        public CreateProductCommandRequest(MenuId menuId, List<AddProductsModelDto> addProductsModelDtos)
        {
            MenuId = menuId;
            AddProductsModelDtos = addProductsModelDtos;
        }
    }
}
