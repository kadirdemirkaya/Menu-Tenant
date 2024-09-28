namespace Shared.Domain.Models.ConnectionPools
{
    public class ConnectionPoolUpdateModel
    {
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TenantId { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string CompnayName { get; set; }
    }
}
