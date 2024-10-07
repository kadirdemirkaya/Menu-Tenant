using Shared.Stream;

namespace NUnit.MenuTest.StreamEvents
{
    public class TestStreamEventHandler : IStreamEventHandler<TestStreamEvent>
    {
        public async Task StreamHandler(TestStreamEvent @event)
        {
            Console.WriteLine(@event.Data);
        }
    }
}
