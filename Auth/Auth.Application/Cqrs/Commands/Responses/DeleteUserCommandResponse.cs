using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.Responses
{
    public class DeleteUserCommandResponse : IEventResponse
    {
        public ApiResponseModel<bool> ApiResponseModel { get; set; }

        public DeleteUserCommandResponse(ApiResponseModel<bool> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
