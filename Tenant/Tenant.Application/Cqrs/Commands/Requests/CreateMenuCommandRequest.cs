using EventBusDomain;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Commands.Requests
{
    public class CreateMenuCommandRequest : IEventRequest
    {
        public CreateMenuModelDto CreateMenuModelDto { get; set; }

        public CreateMenuCommandRequest(CreateMenuModelDto createMenuModelDto)
        {
            CreateMenuModelDto = createMenuModelDto;
        }
    }
}
