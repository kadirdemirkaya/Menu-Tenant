using Shared.Stream;

namespace Auth.Job.Events
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
