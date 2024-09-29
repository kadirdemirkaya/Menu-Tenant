using EventBusDomain;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;

namespace Tenant.Application.Cqrs.Commands.Requests
{
    public class ActiveteMenuCommandRequest : IEventRequest
    {
        public MenuId MenuId { get; set; }

        public ActiveteMenuCommandRequest(Guid menuId)
        {
            MenuId = MenuId.Create(menuId);
        }
    }
}
