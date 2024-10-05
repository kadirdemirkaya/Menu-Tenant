using EventBusDomain;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Models;
using Tenant.Application.Cqrs.Commands.Requests;
using Tenant.Application.Cqrs.Commands.Responses;

namespace Tenant.Application.Cqrs.Commands.RequestHandlers
{
    public class DeleteProductCommandHandler(IRepository<Product, ProductId> _repository) : IEventHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest @event)
        {
            Product product = await _repository.GetAsync(p => p.Id == @event.ProductId, false, false, null);
            product.SetIsDeleted(true);

            bool delResponse = _repository.Update(product);

            if (delResponse)
            {
                delResponse = await _repository.SaveCahangesAsync();

                return new(ApiResponseModel<bool>.CreateSuccess(true));
            }
            return new(ApiResponseModel<bool>.CreateFailure<bool>("a got error in database while product added"));
        }
    }
}
