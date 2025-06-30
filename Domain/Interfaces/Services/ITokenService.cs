
using Domain.Entities;
using Domain.Entities.Jwt;
using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User;
using System.Security.Claims;

namespace Domain.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
        public string GenerateJwtToken(User user, IEnumerable<Claim> customClaims);

        public JwtAccessToken GenerateJwtAccessToken(User user);
        public JwtAccessToken GenerateJwtAccessToken(User user, IEnumerable<Claim> customClaims);
        public RefreshToken GenerateRefreshToken(Guid userId,DeviceFingerprint fingerprint, IpAddress ipAddress, Guid jwtid, bool isRevoked = false, bool isUsed = false);

        public bool IsJwtTokenValid(JwtAccessToken accessToken);
        

        // Security utilities
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public string GetUserIdFromToken(JwtAccessToken accessToken);



    }
}
