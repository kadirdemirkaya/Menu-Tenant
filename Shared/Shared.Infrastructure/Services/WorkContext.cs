using Microsoft.AspNetCore.Http;
using Shared.Application.Abstractions;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Services
{
    public class WorkContext(IHttpContextAccessor _httpContextAccessor) : IWorkContext
    {
        public TenantModel? Tenant
        {
            get
            {
                return _httpContextAccessor?.HttpContext?.Items["TenantModel"] as TenantModel;
            }
        }

        public string CompanyName
        {
            get
            {
                return _httpContextAccessor?.HttpContext?.Items?["CompanyName"]?.ToString() ?? string.Empty;
            }
        }

        public string GetItem(string item)
        {
            return _httpContextAccessor?.HttpContext?.Items[$"{item}"] as string ?? string.Empty;
        }
    }
}
