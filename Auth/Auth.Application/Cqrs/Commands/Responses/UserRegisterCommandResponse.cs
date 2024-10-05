using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.Responses
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
