using EventBusDomain;
using Shared.Domain.Models;

namespace Tenant.Application.Cqrs.Commands.Responses
{
    public class UpdateProductCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel { get; set; }

        public UpdateProductCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
