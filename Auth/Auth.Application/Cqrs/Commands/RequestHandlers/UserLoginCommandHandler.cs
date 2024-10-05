using Auth.Application.Abstractions;
using Auth.Application.Cqrs.Commands.Requests;
using Auth.Application.Cqrs.Commands.Responses;
using EventBusDomain;
using Shared.Domain.Models;
using Shared.Domain.Models.User;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class UserLoginCommandHandler(IUserService _userService) : IEventHandler<UserLoginCommandRequest, UserLoginCommandResponse>
    {
        public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest @event)
        {
            Token? token = await _userService.UserLoginAsync(@event.userLoginModelDto);
            UserLoginModel userLoginModel = new(@event.userLoginModelDto.Email, @event.userLoginModelDto.CompanyName, token);

            return token is not null ? new(ApiResponseModel<UserLoginModel>.CreateSuccess(userLoginModel)) : new(ApiResponseModel<Token>.CreateFailure<UserLoginModel>());
        }
    }
}
