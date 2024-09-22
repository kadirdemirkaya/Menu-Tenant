using Shared.Domain.Models;

namespace Shared.Application.Abstractions
{
    public interface ITokenService
    {
        Token GenerateToken(TenantModel tenantModel);
    }
}
