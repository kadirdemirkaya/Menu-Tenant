namespace Shared.Domain.Models
{
    public class TenantModel
    {
        public string TenantId { get; set; } // companyId
        public string CompanyName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string DatabaseName { get; set; }

        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
    }
}
