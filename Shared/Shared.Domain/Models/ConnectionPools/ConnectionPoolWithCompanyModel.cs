namespace Shared.Domain.Models.ConnectionPools
{
    public class ConnectionPoolWithCompanyModel
    {
        public string CompanyName { get; set; }
        public string DbUrls { get; set; }
        public ConnectionPoolWithCompanyModel(string companyName, string dbUrls)
        {
            CompanyName = companyName;
            DbUrls = dbUrls;
        }
    }
}
