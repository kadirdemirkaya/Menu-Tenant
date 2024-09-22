namespace Shared.Domain.Models
{
    public class TenantModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string TenantId { get; set; } // companyId
        public string Port { get; private set; }
        public string DatabaseName { get; private set; }

        public string Name { get; set; }
        public string Host { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

    }
}
