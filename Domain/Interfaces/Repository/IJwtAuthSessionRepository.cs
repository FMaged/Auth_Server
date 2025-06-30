

using Domain.Entities.Jwt;

namespace Domain.Interfaces.Repository
{
    public interface IJwtAuthSessionRepository
    {
        Task<IEnumerable<JwtAuthSession>> GetAllSessions();

        Task<JwtAuthSession?> GetSessionByIdAsync(Guid sessionId);
        Task<JwtAuthSession?> GetSessionByUserIdAsync(Guid userId);
        Task<JwtAuthSession?> GetSessionByRefreshTokenAsync(string refreshToken);
        Task<bool> AddSessionAsync(JwtAuthSession session);
        Task<bool> UpdateSessionAsync(JwtAuthSession session);
        Task<bool> DeleteSessionAsync(Guid sessionId);

    }
}
