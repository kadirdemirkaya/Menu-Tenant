using Microsoft.AspNetCore.Http;

namespace Shared.Infrastructure.Middlewares
{
    public class CompanyNameMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            if (path.StartsWith("/company/") || path.StartsWith("/Company/"))
            {
                var companyName = path.Split('/')[2];
                context.Items["CompanyName"] = companyName;
            }

            await _next(context);
        }
    }
}
