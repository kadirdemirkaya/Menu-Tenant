using EventBusDomain;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Models;
using Tenant.Application.Abstractions;
using Tenant.Application.Cqrs.Commands.Requests;
using Tenant.Application.Cqrs.Commands.Responses;

namespace Tenant.Application.Cqrs.Commands.RequestHandlers
{
    public class CreateProductCommandHandler(IRepository<Product, ProductId> _repository, IWorkContext _workContext, IImageService imageService) : IEventHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest @event)
        {
            foreach (var productModel in @event.AddProductsModelDtos)
            {
                byte[] image = await imageService.UploadImageAsync(productModel.Image);
                ProductDetail productDetail = ProductDetail.Create(productModel.Description, productModel.WeightInGrams);
                string tenantId = _workContext.Tenant?.TenantId ?? string.Empty;

                Product product = Product.Create(productModel.Title, productModel.Name, productModel.Price, image, productDetail, @event.MenuId, tenantId);

                await _repository.CreateAsync(product);
            }

            bool saveResponse = await _repository.SaveCahangesAsync();

            return saveResponse ? new(ApiResponseModel<bool>.CreateSuccess(true)) : new(ApiResponseModel<bool>.CreateFailure<bool>("got a error in database while added products !"));
        }
    }
}
