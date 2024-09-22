using Microsoft.AspNetCore.Http;
using Shared.Infrastructure.Abstractions;

namespace Shared.Infrastructure.Services
{
    public class WorkContext : IWorkContext
    {
        //#region Fields

        //private readonly IHttpContextAccessor _httpContextAccessor;

        //#endregion

        //#region Ctor

        //public WorkContext(IHttpContextAccessor httpContextAccessor)
        //{
        //    this._httpContextAccessor = httpContextAccessor;
        //}

        //#endregion

        //#region Property

        //public TenantInfo? Tenant
        //{
        //    get
        //    {
        //        return _httpContextAccessor?.HttpContext?.Items["TenantInfo"] as TenantInfo;
        //    }
        //}

        //#endregion
    }
}
