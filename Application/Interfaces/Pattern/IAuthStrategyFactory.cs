
using Domain.ValueObjects.Cookie;

namespace Application.Interfaces.Pattern
{
    public interface IAuthStrategyFactory
    {




        /// <summary>
        /// Creates an authentication strategy based on the provided type.
        /// </summary>
        /// <param name="type">The type of authentication strategy to create.</param>
        /// <returns>An instance of the specified authentication strategy.</returns>
        IAuthStrategy CreateAuthStrategy(string type);
        /// <summary>
        /// Gets the cookie options for the authentication strategy.
        /// </summary>
        /// <returns>The cookie options used by the authentication strategy.</returns>
        CookieOptions GetCookieOptions();
    }
}
