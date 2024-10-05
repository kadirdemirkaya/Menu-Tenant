using Auth.Application.Cqrs.Commands.Requests;
using Auth.Application.Cqrs.Commands.Responses;
using EventBusDomain;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class DeleteUserCommandHandler(IRepository<AppUser, AppUserId> _repository) : IEventHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest @event)
        {
            AppUser? user = await _repository.GetAsync(u => u.Id == AppUserId.Create(@event.Id), false, true);

            if (user is not null)
            {
                user.SetIsDeleted(true);
                user.SetUpdatedDateUTC(DateTime.UtcNow);

                bool updateRes = _repository.Update(user);

                return updateRes is true ? new(ApiResponseModel<bool>.CreateSuccess(true)) : new(ApiResponseModel<bool>.CreateFailure<bool>("A got error in update process"));
            }

            // TODO : Request event can be sending to database service in this section 

            return new(ApiResponseModel<bool>.CreateFailure<bool>("User is not found !"));
        }
    }
}
