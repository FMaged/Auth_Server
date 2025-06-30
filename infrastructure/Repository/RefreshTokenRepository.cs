using Domain.Entities.Jwt;
using Domain.Interfaces.Repository;
using Domain.ValueObjects.User;

namespace infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {


        public Task<RefreshToken?> GetByTokenAsync(string token)
        {

            throw new NotImplementedException();

        }



        public Task<bool> AddTokenAsync(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CleanRevokedTokensAsync(int daysToKeep = 30)
        {
            throw new NotImplementedException();
        }

        public Task CleanUpExpiredAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RefreshToken>?> GetAllTokensAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RefreshToken>?> GetAllTokensByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }


        public Task<RefreshToken?> GetByTokenStringAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> GetLastTokenByUserIdAsync(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken?> GetTokenByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateAllAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRevokedAsync(Guid tokenId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task MarkAsUsedAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RefreshToken token)
        {
            throw new NotImplementedException();
        }

        public Task<List<RefreshToken>> GetByDeviceFingerPrint(DeviceFingerprint deviceFingerprint)
        {
             throw new NotImplementedException();
        }
    }
}
