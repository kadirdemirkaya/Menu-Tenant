using Shared.Domain.Models;

namespace Shared.Application.Abstractions
{
    public interface IWorkContext
    {
        public string CompanyName { get; }
        public string GetItem(string item);
        public TenantModel? Tenant { get; }
    }
}
