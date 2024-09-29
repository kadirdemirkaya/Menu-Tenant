using EventBusDomain;
using Shared.Domain.Models;

namespace Tenant.Application.Cqrs.Commands.Responses
{
    public class ActiveteMenuCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel { get; set; }

        public ActiveteMenuCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
