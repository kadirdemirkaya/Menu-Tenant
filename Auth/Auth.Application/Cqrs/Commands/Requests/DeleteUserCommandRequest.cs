using EventBusDomain;

namespace Auth.Application.Cqrs.Commands.Requests
{
    public class DeleteUserCommandRequest : IEventRequest
    {
        public Guid Id { get; set; }

        public DeleteUserCommandRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
