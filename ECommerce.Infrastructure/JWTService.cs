using System;

namespace ECommerce.Infrastructure
{
    public class JWTService
    {
        public string GenerateToken(string userId)
        {
            // TODO: Implement JWT token generation
            return $"token-for-{userId}";
        }
    }
}
