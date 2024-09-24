using Auth.Application.Abstractions;
using Auth.Application.Cqrs.Commands.RequestsAndResponses;
using EventBusDomain;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class UserLoginCommandHandler(IUserService _userService) : IEventHandler<UserLoginCommandRequest, UserLoginCommandResponse>
    {
        public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest @event)
        {
            Token? token = await _userService.UserLoginAsync(@event.userLoginModelDto);

            return token is not null ? new(ApiResponseModel<Token>.CreateSuccess(token)) : new(ApiResponseModel<Token>.CreateFailure<Token>());
        }
    }
}
