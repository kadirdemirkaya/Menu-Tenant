using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.Application.Abstractions;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtTokenService _authService;
        public JwtMiddleware(RequestDelegate next, IJwtTokenService authService)
        {
            _next = next;
            _authService = authService;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                bool result = _authService.ValidateCurrentToken(token);

                if (result == false)
                {
                    return;
                }

                var tenantModel = _authService.GetClaim(token, "TenantModel");

                if (string.IsNullOrWhiteSpace(tenantModel))
                    return;

                context.Items["TenantModel"] = JsonConvert.DeserializeObject<TenantModel>(tenantModel);
            }

            await _next(context);
        }
    }
}
