using Shared.Stream;

namespace Database.EventGateway.Events
{
    public class DatabaseCreatedProcessStreamEvent : IStreamEvent
    {
        public string TenantId { get; set; }
        public bool IsDbCreated { get; set; }

        public DatabaseCreatedProcessStreamEvent(string tenantId, bool isDbCreated)
        {
            TenantId = tenantId;
            IsDbCreated = isDbCreated;
        }

    }
}

