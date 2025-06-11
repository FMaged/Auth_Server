using Domain.Entities.Jwt;

namespace Domain.Interfaces.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>?> GetAllTokensAsync();
        Task<IEnumerable<RefreshToken>?> GetAllTokensByUserIdAsync(Guid userId);
        Task<RefreshToken?> GetLastTokenByUserIdAsync(Guid UserId);
        Task<RefreshToken?> GetTokenByIdAsync(Guid id);
        Task<bool> AddTokenAsync(RefreshToken refreshToken);
        Task RevokeAsync(Guid id);
        Task MarkAsUsedAsync(Guid id);

        Task CleanUpExpiredAsync();
        Task InvalidateAllAsync(Guid userId);


        // Helper methods
        Task<bool> IsValidAsync(string token);
        Task<bool> IsRevokedAsync(Guid tokenId);
        Task<RefreshToken?> GetByTokenStringAsync(string token);


    }
}
