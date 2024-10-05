namespace Shared.Domain.Models.ConnectionPools
{
    public class ConnectionPoolWithCompanyModel
    {
        public string CompanyName { get; set; }
        public string DbUrls { get; set; }
        public string TenantId { get; set; }
        public ConnectionPoolWithCompanyModel(string companyName, string dbUrls, string tenantId)
        {
            CompanyName = companyName;
            DbUrls = dbUrls;
            TenantId = tenantId;
        }
    }
}
