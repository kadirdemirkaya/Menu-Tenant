using EventBusDomain;
using NUnit.MenuTest.Events.Event;
using NUnit.MenuTest.Events.Response;

namespace NUnit.MenuTest.Events.EventHandler
{
    public class MenuTestEventHandler : IEventHandler<MenuTestEventRequest, MenuTestEventResponse>
    {
        public async Task<MenuTestEventResponse> Handle(MenuTestEventRequest @event)
        {
            return new MenuTestEventResponse() { Response = @event.Message };
        }
    }
}
