using EventBusDomain;
using Shared.Domain.Models;

namespace Tenant.Application.Cqrs.Commands.Responses
{
    public class CreateProductCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel { get; set; }

        public CreateProductCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
