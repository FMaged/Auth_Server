

namespace Application.Common.Exceptions
{
    public class SecurityTokenException:AuthException
    {
        public SecurityTokenException() { }
        public SecurityTokenException(string message) : base(message) { }
        public SecurityTokenException(string message, Exception inner) : base(message, inner) { }
    }
}
