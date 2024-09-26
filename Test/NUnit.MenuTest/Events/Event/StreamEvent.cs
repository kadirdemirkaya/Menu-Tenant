using Shared.Stream;

namespace NUnit.MenuTest.Events.Event
{
    public class StreamEvent : IStreamEvent
    {
        public string Message { get; set; }
    }
}
