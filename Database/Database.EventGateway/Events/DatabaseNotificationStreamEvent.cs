using Shared.Stream;

namespace Database.EventGateway.Events
{
    public class DatabaseNotificationStreamEvent : IStreamEvent
    {
        public List<string> ConnectionStrings { get; set; }

        public DatabaseNotificationStreamEvent(List<string> connectionStrings)
        {
            ConnectionStrings = connectionStrings;
        }
    }
}
