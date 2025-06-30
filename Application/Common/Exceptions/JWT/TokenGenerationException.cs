
namespace Application.Common.Exceptions.JWT
{
    public class TokenGenerationException:AuthException
    {
        public TokenGenerationException() { }
        public TokenGenerationException(string message) : base(message) { }
        public TokenGenerationException(string message, Exception inner) : base(message, inner) { }
    }
}
