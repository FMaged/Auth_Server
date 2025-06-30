

namespace Application.Common.Exceptions.User
{
    public class UserLockedOutException: AuthException
    {

        public UserLockedOutException() { }
        public UserLockedOutException(string message) : base(message) { }
        public UserLockedOutException(string message, Exception inner) : base(message, inner) { }
    }
}
