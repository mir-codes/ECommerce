using System.Collections.Generic;
using System.Security.Claims;

namespace ECommerce.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
