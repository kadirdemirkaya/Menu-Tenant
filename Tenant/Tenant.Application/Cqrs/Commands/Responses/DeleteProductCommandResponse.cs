using EventBusDomain;
using Shared.Domain.Models;

namespace Tenant.Application.Cqrs.Commands.Responses
{
    public class DeleteProductCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel { get; set; }

        public DeleteProductCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
