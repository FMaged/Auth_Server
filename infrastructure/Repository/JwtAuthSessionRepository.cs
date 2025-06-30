

using Domain.Entities.Jwt;
using Domain.Interfaces.Repository;

namespace infrastructure.Repository
{
    public class JwtAuthSessionRepository : IJwtAuthSessionRepository
    {
        public async Task<bool> AddSessionAsync(JwtAuthSession session)
            {
           return await Task.FromResult(true); // Simulate adding session to a data store
        }

        public Task<bool> DeleteSessionAsync(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JwtAuthSession>> GetAllSessions()
        {
            throw new NotImplementedException();
        }

        public Task<JwtAuthSession?> GetSessionByIdAsync(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<JwtAuthSession?> GetSessionByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<JwtAuthSession?> GetSessionByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSessionAsync(JwtAuthSession session)
        {
            throw new NotImplementedException();
        }
    }
}
