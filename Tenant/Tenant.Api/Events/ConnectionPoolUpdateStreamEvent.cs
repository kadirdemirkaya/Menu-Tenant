using Shared.Stream;

namespace Tenant.Api.Events
{
    public class ConnectionPoolUpdateStreamEvent : IStreamEvent
    {
        public string Message { get; set; }
    }
}
