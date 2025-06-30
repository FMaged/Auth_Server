

namespace Application.Common.Exceptions.JWT
{
    public class InvalidJwtSessionException : AuthException
    {
        public InvalidJwtSessionException() { }
        public InvalidJwtSessionException(string message) : base(message) { }
        public InvalidJwtSessionException(string message, Exception inner) : base(message, inner) { }
    }
}
