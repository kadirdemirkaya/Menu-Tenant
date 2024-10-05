using Auth.Application.Dtos.User;
using EventBusDomain;

namespace Auth.Application.Cqrs.Commands.Requests
{
    public class UserRegisterCommandRequest : IEventRequest
    {
        public UserRegisterModelDto userRegisterModelDto { get; set; }

        public UserRegisterCommandRequest(UserRegisterModelDto userRegisterModelDto)
        {
            this.userRegisterModelDto = userRegisterModelDto;
        }
    }
}
