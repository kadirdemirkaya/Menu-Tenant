using Microsoft.AspNetCore.Http;

namespace Shared.Infrastructure.Middlewares
{
    public class CompanyNameMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            String path = context.Request.Path.Value ?? string.Empty;

            if (path.Contains("/company/"))
            {
                var companyId = path.Split("/company/").LastOrDefault();
                if (!string.IsNullOrEmpty(companyId))
                {
                    context.Items["CompanyName"] = companyId;
                }
            }

            await _next(context);
        }
    }
}
