using Auth.Application.Abstractions;
using Auth.Application.Cqrs.Commands.Requests;
using Auth.Application.Cqrs.Commands.Responses;
using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class UserRegisterCommandHandler(IUserService _userService) : IEventHandler<UserRegisterCommandRequest, UserRegisterCommandResponse>
    {
        public async Task<UserRegisterCommandResponse> Handle(UserRegisterCommandRequest @event)
        {
            bool response = await _userService.UserRegisterAsync(@event.userRegisterModelDto);

            return response is true ? new UserRegisterCommandResponse(ApiResponseModel<bool>.CreateSuccess(true)) : new UserRegisterCommandResponse(ApiResponseModel<bool>.CreateFailure<bool>());
        }
    }
}
