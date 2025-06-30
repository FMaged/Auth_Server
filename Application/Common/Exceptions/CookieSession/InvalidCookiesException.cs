

namespace Application.Common.Exceptions.CookieSession
{
    public class InvalidCookiesException:AuthException
    {
        public InvalidCookiesException() { }
        public InvalidCookiesException(string message) : base(message) { }
        public InvalidCookiesException(string message, Exception inner) : base(message, inner) { }
    }
}
