

using Domain.Entities.Cookie;
using Domain.Interfaces.Repository;

namespace infrastructure.Repository
{
    public class CookieSessionRepository : ICookieSessionRepository
    {
        public Task<bool> AddSessionAsync(CookieAuthSession session)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSessionAsync(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CookieAuthSession>> GetAllSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CookieAuthSession>> GetAllSessionsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CookieAuthSession?> GetSessionByIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSessionAsync(Guid sessionId, CookieAuthSession session)
        {
            throw new NotImplementedException();
        }
    }
}
