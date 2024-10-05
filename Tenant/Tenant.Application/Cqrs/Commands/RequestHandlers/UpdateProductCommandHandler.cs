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
    public class UpdateProductCommandHandler(IRepository<Product, ProductId> _repository, IImageService _imageService) : IEventHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest @event)
        {
            bool anyResponse = await _repository.AnyAsync(p => p.Id == ProductId.Create(@event.UpdateProductDto.ProductId));

            if (anyResponse)
            {
                ProductDetail productDetail = ProductDetail.Create(@event.UpdateProductDto.Description, @event.UpdateProductDto.WeightInGrams ?? 0.0);

                byte[] imageByte = await _imageService.UploadImageAsync(@event.UpdateProductDto.Image);

                Product product = Product.Create(@event.UpdateProductDto.Title, @event.UpdateProductDto.Name, @event.UpdateProductDto.Price, imageByte, productDetail, MenuId.Create(@event.UpdateProductDto.MenuId)); // tenantId gotta added automaticly at savechanges !

                bool updateResponse = _repository.Update(product);

                if (updateResponse)
                    updateResponse = await _repository.SaveCahangesAsync();

                return updateResponse is true ? new(ApiResponseModel<bool>.CreateSuccess(true)) : new(ApiResponseModel<bool>.CreateFailure<bool>("got a error while product updated"));
            }

            return new(ApiResponseModel<bool>.CreateNotFound<bool>("No object to update found !"));
        }
    }
}
