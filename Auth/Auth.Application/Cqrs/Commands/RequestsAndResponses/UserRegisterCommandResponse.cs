using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestsAndResponses
{
    public class UserRegisterCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel;

        public UserRegisterCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
