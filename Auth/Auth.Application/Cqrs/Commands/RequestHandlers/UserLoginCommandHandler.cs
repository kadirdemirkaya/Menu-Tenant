using Auth.Application.Cqrs.Commands.RequestsAndResponses;
using EventBusDomain;

namespace Auth.Application.Cqrs.Commands.RequestHandlers
{
    public class UserLoginCommandHandler : IEventHandler<UserLoginCommandRequest, UserLoginCommandResponse>
    {
        public Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest @event)
        {
            throw new NotImplementedException();
        }
    }
}
