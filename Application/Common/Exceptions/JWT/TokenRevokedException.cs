

namespace Application.Common.Exceptions.JWT
{
    public class TokenRevokedException: AuthException
    {
        public TokenRevokedException() { }
        public TokenRevokedException(string message) : base(message) { }
        public TokenRevokedException(string message, Exception inner) : base(message, inner) { }

    }
}
