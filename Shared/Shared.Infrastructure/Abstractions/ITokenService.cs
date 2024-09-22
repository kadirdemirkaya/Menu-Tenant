using Newtonsoft.Json.Linq;
using Shared.Domain.Models;

namespace Shared.Infrastructure.Abstractions
{
    public interface ITokenService
    {
        Token GenerateToken(TenantModel tenantModel);
    }
}
