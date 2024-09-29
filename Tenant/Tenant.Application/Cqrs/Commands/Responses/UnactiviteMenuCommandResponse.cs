using EventBusDomain;
using Shared.Domain.Models;

namespace Tenant.Application.Cqrs.Commands.Responses
{
    public class UnactiviteMenuCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel { get; set; }

        public UnactiviteMenuCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
