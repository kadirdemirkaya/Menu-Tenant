using EventBusDomain;
using Shared.Application.Models.Dtos.User;

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
