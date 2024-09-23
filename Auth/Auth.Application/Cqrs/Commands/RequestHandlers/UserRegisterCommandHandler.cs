using Auth.Application.Cqrs.Commands.RequestsAndResponses;
using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class UserRegisterCommandHandler : IEventHandler<UserRegisterCommandRequest, UserRegisterCommandResponse>
    {
        public async Task<UserRegisterCommandResponse> Handle(UserRegisterCommandRequest @event)
        {



            return new UserRegisterCommandResponse(ApiResponseModel<bool>.CreateSuccess(true));
        }
    }
}
