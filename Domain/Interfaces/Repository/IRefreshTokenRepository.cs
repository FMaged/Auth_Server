using Domain.Entities.Jwt;
using Domain.ValueObjects.User;

namespace Domain.Interfaces.Repository
{
    public interface IRefreshTokenRepository
    {
        // Core operations
        Task<IEnumerable<RefreshToken>?> GetAllTokensAsync();
        Task<IEnumerable<RefreshToken>?> GetAllTokensByUserIdAsync(Guid userId);
        Task<RefreshToken?> GetLastTokenByUserIdAsync(Guid UserId);
        Task<RefreshToken?> GetTokenByIdAsync(Guid id);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<bool> AddTokenAsync(RefreshToken refreshToken);
        Task<bool> UpdateAsync(RefreshToken token);
        Task<bool> DeleteAsync(string token);


        // Token management


        Task RevokeAsync(Guid id);
        Task MarkAsUsedAsync(Guid id);

        Task CleanUpExpiredAsync();
        Task<int> CleanRevokedTokensAsync(int daysToKeep = 30);
        Task InvalidateAllAsync(Guid userId);


        // Helper methods
        Task<bool> IsValidAsync(string token);
        Task<bool> IsRevokedAsync(Guid tokenId);
        Task<RefreshToken?> GetByTokenStringAsync(string token);
        public Task<List<RefreshToken>> GetByDeviceFingerPrint(DeviceFingerprint deviceFingerprint);


    }
}
