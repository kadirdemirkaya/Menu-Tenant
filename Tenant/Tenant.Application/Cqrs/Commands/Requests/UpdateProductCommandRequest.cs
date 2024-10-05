using EventBusDomain;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Commands.Requests
{
    public class UpdateProductCommandRequest : IEventRequest
    {
        public UpdateProductDto UpdateProductDto { get; set; }

        public UpdateProductCommandRequest(UpdateProductDto updateProductDto)
        {
            UpdateProductDto = updateProductDto;
        }
    }
}
