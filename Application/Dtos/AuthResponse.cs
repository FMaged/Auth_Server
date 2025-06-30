

using Domain.Entities.Jwt;
using Domain.ValueObjects.Cookie;
using Domain.ValueObjects.JWT;
using System.ComponentModel;

namespace Application.Dtos
{
    public class AuthResponse
    {
        public JwtAccessToken? AccessToken { get; init; }
        public RefreshToken? RefreshToken { get; init; }
        public string? Cookie { get; init; }

        public bool IsJwtBased => RefreshToken != null;
        public bool IsCookieBased => Cookie != null;

        public AuthResponse() { }
        public AuthResponse(JwtAccessToken? accessToken, RefreshToken? refreshToken, string? cookie)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Cookie=cookie;
        }
    }
}
