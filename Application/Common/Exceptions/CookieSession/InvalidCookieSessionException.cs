

namespace Application.Common.Exceptions.CookieSession
{
    public class InvalidCookieSessionException : AuthException
    {
        public InvalidCookieSessionException() { }
        public InvalidCookieSessionException(string message) : base(message) { }
        public InvalidCookieSessionException(string message, Exception inner) : base(message, inner) { }
    }
}
