using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestsAndResponses
{
    public class UserLoginCommandResponse : IEventResponse
    {
        public ApiResponseModel<Token> ApiResponseModel { get; set; }

        public UserLoginCommandResponse(ApiResponseModel<Token> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
