using EventBusDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Tenant.Application.Cqrs.Commands.Requests;
using Tenant.Application.Cqrs.Commands.Responses;
using Tenant.Application.Dtos;

namespace Tenant.Api.Controllers
{
    [Authorize]
    public class ProductController(EventBus _eventBus) : BaseController
    {
        [HttpPost]
        [Route("createproduct")]
        public async Task<IActionResult> CreateProduct([FromHeader] Guid menuId, [FromForm] List<AddProductsModelDto> addProductsModelDtos)
        {
            CreateProductCommandRequest request = new(MenuId.Create(menuId), addProductsModelDtos);
            CreateProductCommandResponse response = await _eventBus.PublishAsync(request) as CreateProductCommandResponse;

            return response.ApiResponseModel.Success ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("deleteproduct")]
        public async Task<IActionResult> DeleteProduct([FromHeader] Guid productId)
        {
            DeleteProductCommandRequest request = new(ProductId.Create(productId));
            DeleteProductCommandResponse response = await _eventBus.PublishAsync(request) as DeleteProductCommandResponse;

            return response.ApiResponseModel.Success ? Ok() : BadRequest();
        }

        [HttpPut]
        [Route("updateproduct")]
        public async Task<IActionResult> UpdateProduct()
        {
            UpdateProductCommandRequest request = new(default);
            UpdateProductCommandResponse response = await _eventBus.PublishAsync(request) as UpdateProductCommandResponse;
            
            return response.ApiResponseModel.Success ? Ok() : BadRequest();
        }
    }
}
