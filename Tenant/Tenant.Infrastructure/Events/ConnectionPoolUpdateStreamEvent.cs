using Shared.Stream;

namespace Tenant.Infrastructure.Events
{
    public class ConnectionPoolUpdateStreamEvent : IStreamEvent
    {
        public string Message { get; set; }
    }
}
