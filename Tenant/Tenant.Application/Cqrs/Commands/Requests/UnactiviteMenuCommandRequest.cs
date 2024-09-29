using EventBusDomain;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;

namespace Tenant.Application.Cqrs.Commands.Requests
{
    public class UnactiviteMenuCommandRequest : IEventRequest
    {
        public MenuId MenuId { get; set; }

        public UnactiviteMenuCommandRequest(Guid menuId)
        {
            MenuId = MenuId.Create(menuId);
        }
    }
}
