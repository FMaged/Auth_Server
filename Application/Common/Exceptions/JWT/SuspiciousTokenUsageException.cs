

namespace Application.Common.Exceptions.JWT
{
    public class SuspiciousTokenUsageException:AuthException
    {
        public SuspiciousTokenUsageException() { }
        public SuspiciousTokenUsageException(string message) : base(message) { }
        public SuspiciousTokenUsageException(string message, Exception inner) : base(message, inner) { }
    }
}
