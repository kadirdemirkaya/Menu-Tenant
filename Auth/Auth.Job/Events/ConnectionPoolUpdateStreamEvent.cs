using Shared.Domain.Models.ConnectionPools;
using Shared.Stream;

namespace Auth.Job.Events
{
    public class ConnectionPoolUpdateStreamEvent : IStreamEvent
    {
        public ConnectionPoolUpdateModel ConnectionPoolUpdate { get; set; }

        public ConnectionPoolUpdateStreamEvent(ConnectionPoolUpdateModel connectionPoolUpdate)
        {
            ConnectionPoolUpdate = connectionPoolUpdate;
        }
    }
}
