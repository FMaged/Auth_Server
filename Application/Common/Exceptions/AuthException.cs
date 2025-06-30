

namespace Application.Common.Exceptions
{
    public abstract class AuthException:Exception
    {
        public AuthException() { }
        public AuthException(string message) : base(message) { }
        public AuthException(string message, Exception inner) : base(message, inner) { }
    }
}
