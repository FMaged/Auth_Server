

namespace Application.Common.Exceptions.JWT
{
    public class TokenQuotaExceededException:AuthException
    {
        public TokenQuotaExceededException() { }
        public TokenQuotaExceededException(string message) : base(message) { }
        public TokenQuotaExceededException(string message, Exception inner) : base(message, inner) { }
    }
}
