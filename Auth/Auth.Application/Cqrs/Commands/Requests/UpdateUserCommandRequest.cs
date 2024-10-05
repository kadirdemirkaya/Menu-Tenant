using Auth.Application.Dtos.User;
using EventBusDomain;

namespace Auth.Application.Cqrs.Commands.Requests
{
    public class UpdateUserCommandRequest : IEventRequest
    {
        public UserUpdateModelDto userUpdateModelDto { get; set; }

        public UpdateUserCommandRequest(UserUpdateModelDto userUpdateModelDto)
        {
            userUpdateModelDto = userUpdateModelDto;
        }
    }
}
