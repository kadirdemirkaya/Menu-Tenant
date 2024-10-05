using EventBusDomain;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;

namespace Tenant.Application.Cqrs.Commands.Requests
{
    public class DeleteProductCommandRequest : IEventRequest
    {
        public ProductId ProductId { get; set; }

        public DeleteProductCommandRequest(ProductId productId)
        {
            ProductId = productId;
        }
    }
}
