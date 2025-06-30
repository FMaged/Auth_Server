

namespace Application.Common.Exceptions.JWT
{
    public class InvalidRefreshTokenException:AuthException
    {
        public InvalidRefreshTokenException() { }
        public InvalidRefreshTokenException(string message) : base(message) { }
        public InvalidRefreshTokenException(string message, Exception inner) : base(message, inner) { }
    }
}
