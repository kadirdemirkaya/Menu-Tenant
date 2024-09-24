using Auth.Application.Dtos.User;
using EventBusDomain;

namespace Auth.Application.Cqrs.Commands.RequestsAndResponses
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
