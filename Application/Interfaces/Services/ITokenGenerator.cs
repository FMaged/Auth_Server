using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();

        bool IsAccessTokenValid(string token);
        bool IsRefreshTokenValid(string refreshToken);

        string GetUserId(string token);
        string GetEmail(string token);
        string GetUserName(string token);
    }
}
