

using Domain.Entities.Jwt;

namespace Domain.Interfaces.Repository
{
    public interface IJwtAuthSessionRepository
    {
        Task<IEnumerable<JwtAuthSession>> GetAllSessions();

        Task<JwtAuthSession?> GetSessionByIdAsync(Guid sessionId);
        Task<JwtAuthSession?> GetSessionByUserIdAsync(Guid userId);
        Task<JwtAuthSession?> GetSessionByJwtIdAsync(string refreshToken);
        Task<bool> AddSessionAsync(JwtAuthSession session);
        Task<bool> UpdateSessionAsync(Guid sessionId, JwtAuthSession session);
        Task<bool> DeleteSessionAsync(Guid sessionId);

    }
}
