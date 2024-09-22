using Shared.Domain.Models;

namespace Shared.Application.Abstractions
{
    public interface IJwtTokenService
    {
        string GenerateToken(TenantModel tenantInfo);
        bool ValidateCurrentToken(string token);
        string GetClaim(string token, string claimType);
    }
}
