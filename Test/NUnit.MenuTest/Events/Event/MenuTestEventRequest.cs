using EventBusDomain;

namespace NUnit.MenuTest.Events.Event
{
    public class MenuTestEventRequest : IEventRequest
    {
        public string  Message { get; set; }
    }
}
