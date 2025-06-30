
using Domain.ValueObjects.Cookie;

namespace Domain.Interfaces.Services
{
    public interface ICookieService
    {
        public string GenerateSecureToken(int byteLength = 64);
        public string BuildCookieHeader(CookieToken cookieToken);
        public CookieToken ParseCookieHeader(string cookieHeader);
        public bool IsValid(CookieToken cookieToken);


    }
}
