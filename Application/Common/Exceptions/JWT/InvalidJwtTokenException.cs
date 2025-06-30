

namespace Application.Common.Exceptions.JWT
{
    public class InvalidJwtTokenException:AuthException
    {
        public InvalidJwtTokenException() { }
        public InvalidJwtTokenException(string message) : base(message) { }
        public InvalidJwtTokenException(string message, Exception inner) : base(message, inner) { }
    }
}
