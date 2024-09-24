using EventBusDomain;

namespace NUnit.MenuTest.Events.Response
{
    public class MenuTestEventResponse : IEventResponse
    {
        public string Response { get; set; }
    }
}
