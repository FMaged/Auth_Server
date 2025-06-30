

namespace Application.Common.Exceptions.User
{
    public class UserNameFormatException : AuthException
    {
        public UserNameFormatException() { }
        public UserNameFormatException(string message) : base(message) { }
        public UserNameFormatException(string message, Exception inner) : base(message, inner) { }
    }
}
