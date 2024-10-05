using Auth.Application.Cqrs.Commands.Requests;
using Auth.Application.Cqrs.Commands.Responses;
using Auth.Application.Dtos.User;
using EventBusDomain;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class UpdateUserCommandHandler(IRepository<AppUser, AppUserId> _repository) : IEventHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest @event)
        {
            AppUser? currentUser = await _repository.GetAsync(null, true, false);

            if (currentUser is not null)
            {
                currentUser.UpdateUser(@event.userUpdateModelDto.username, @event.userUpdateModelDto.email, @event.userUpdateModelDto.password, @event.userUpdateModelDto.phoneNumber);

                bool updateRes = _repository.Update(currentUser);

                UserModelDto userModelDto = new();
                userModelDto = userModelDto.UserModelDtoMapper(currentUser);

                return updateRes is true ? new(ApiResponseModel<UserModelDto>.CreateSuccess(userModelDto)) : new(ApiResponseModel<UserModelDto>.CreateServerError<UserModelDto>());
            }

            return new(ApiResponseModel<UserModelDto>.CreateFailure<UserModelDto>());
        }
    }
}
