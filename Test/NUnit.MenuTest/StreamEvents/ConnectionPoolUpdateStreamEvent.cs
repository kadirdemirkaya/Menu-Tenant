using Shared.Stream;

namespace NUnit.MenuTest.StreamEvents
{
    public class ConnectionPoolUpdateStreamEvent : IStreamEvent
    {
        public string Message { get; set; }
    }
}
