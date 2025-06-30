

using Domain.Entities.Cookie;

namespace Domain.Interfaces.Repository
{
    public interface ICookieSessionRepository
    {





        /// <summary>
        /// Retrieves all cookie sessions.
        /// </summary>
        /// <returns>A collection of all cookie sessions.</returns>
        Task<IEnumerable<CookieAuthSession>> GetAllSessionsAsync();
        /// <summary>
        /// Retrieves all cookie sessions for the user.
        /// </summary>
        /// <returns>A collection of all cookie sessions.</returns>
        Task<IEnumerable<CookieAuthSession>> GetAllSessionsByUserIdAsync(Guid userId);



        /// <summary>
        /// Retrieves a cookie session by its ID.
        /// </summary>
        /// <param name="sessionId">The ID of the session.</param>
        /// <returns>The cookie session if found, otherwise null.</returns>
        Task<CookieAuthSession?> GetSessionByIdAsync(string sessionId);




        /// <summary>
        /// Adds a new cookie session.
        /// </summary>
        /// <param name="session">The cookie session to add.</param>
        /// <returns>True if the session was added successfully, otherwise false.</returns>
        Task<bool> AddSessionAsync(CookieAuthSession session);
        /// <summary>
        /// Updates an existing cookie session.
        /// </summary>
        /// <param name="sessionId">The ID of the session to update.</param>
        /// <param name="session">The updated cookie session data.</param>
        /// <returns>True if the session was updated successfully, otherwise false.</returns>
        Task<bool> UpdateSessionAsync(Guid sessionId, CookieAuthSession session);
        /// <summary>
        /// Deletes a cookie session by its ID.
        /// </summary>
        /// <param name="sessionId">The ID of the session to delete.</param>
        /// <returns>True if the session was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteSessionAsync(string sessionId);
    }
}
