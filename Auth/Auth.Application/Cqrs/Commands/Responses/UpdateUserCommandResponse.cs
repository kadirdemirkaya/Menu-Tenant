using Auth.Application.Dtos.User;
using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.Responses
{
    public class UpdateUserCommandResponse : IEventResponse
    {
        public ApiResponseModel<UserModelDto> ApiResponseModel { get; set; }

        public UpdateUserCommandResponse(ApiResponseModel<UserModelDto> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
