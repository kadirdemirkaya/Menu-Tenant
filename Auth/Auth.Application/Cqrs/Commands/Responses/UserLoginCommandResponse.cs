using EventBusDomain;
using Shared.Domain.Models;
using Shared.Domain.Models.User;

namespace Auth.Application.Cqrs.Commands.Responses
{
    public class UserLoginCommandResponse : IEventResponse
    {
        public ApiResponseModel<UserLoginModel> ApiResponseModel { get; set; }

        public UserLoginCommandResponse(ApiResponseModel<UserLoginModel> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
