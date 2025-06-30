using Application.Common.Exceptions.CookieSession;
using Domain.Interfaces.Services;
using Domain.ValueObjects.Cookie;
using Domain.Enums;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace infrastructure.Services
{
    public class CookieService : ICookieService
    {
        
        public string GenerateSecureToken(int byteLength = 64)
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[byteLength];
            rng.GetBytes(randomBytes);
            string tokenString = Convert.ToBase64String(randomBytes);
            return tokenString.TrimEnd('=').Replace('+', '-').Replace('/', '_'); // URL-safe base64 encoding
        }


        public CookieToken ParseCookieHeader(string cookieHeader)
        {
            if (string.IsNullOrEmpty(cookieHeader))
                throw new InvalidCookiesException("The header can not be empty");

            CookieOptions options = new CookieOptions();
            var extensions = new List<string>();






            string[] cookies=cookieHeader.Split(';',StringSplitOptions.TrimEntries);
            string[] nameValue= cookies[0].Split("=",2);
            string name= nameValue[0].Trim();
            string SessionId= nameValue[1].Trim();
            foreach (string part in cookies.Skip(1))
            {
                if (string.IsNullOrEmpty(part)) continue;
                var keyValue = part.Split('=', 2, StringSplitOptions.TrimEntries);
                var key = keyValue[0];
                var value = keyValue.Length > 1 ? keyValue[1] : string.Empty;
                switch (key.ToLowerInvariant())
                {
                    case "expires" when value != null:
                        if (!DateTime.TryParseExact(value, "R", CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeUniversal, out var exp))
                            throw new InvalidCookiesException("Invalid expiration date format");
                        options.Expires = exp.ToUniversalTime();
                        break;
                    case "domain" when value != null:
                        options.Domain = value;
                        break;
                    case "path" when value != null:
                        options.Path = value;
                        break;
                    case "secure":
                        options.Secure = true;
                        break;
                    case "httponly":
                        options.HttpOnly = true;
                        break;
                    case "isessential":
                        options.IsEssential = true;
                        break;
                    case "samesite" when value != null:
                        if (!Enum.TryParse<SameSiteType>(value, true, out var sameSite))
                            throw new InvalidCookiesException("Invalid SameSite value");
                        options.SameSite = sameSite;
                        break;
                    default:
                        extensions.Add(value != null ? $"{key}={value}" : key);
                        break;
                }
            }
            if (extensions.Count > 0)
                options.Extensions = extensions;
            var cookieResult=CookieToken.Create(SessionId, options, name);
            if(!cookieResult.IsSuccess)
                throw new InvalidCookiesException(cookieResult.Error);
            return cookieResult.Value;



        }

        public bool IsValid(CookieToken cookieToken)
        {
            if (string.IsNullOrWhiteSpace(cookieToken.SessionId) ||
                !CookieHelpers.TokenRegex.IsMatch(cookieToken.SessionId))
                return false;

            if (cookieToken.Options.Expires < DateTime.UtcNow)
                return false;

            return true;

        }

        public string BuildCookieHeader(CookieToken cookieToken)
        {
            var builder=new StringBuilder();
            builder.Append($"{cookieToken.Name}={cookieToken.SessionId}; ");

            // RFC1123 expiration format
            builder.Append($"Expires={cookieToken.Options.Expires?.ToUniversalTime():R}; ");

            if (!string.IsNullOrEmpty(cookieToken.Options.Domain))
                builder.Append($"Domain={cookieToken.Options.Domain}; ");

            if(!string.IsNullOrEmpty(cookieToken.Options.Path))
                builder.Append($"Path={cookieToken.Options.Path}; ");



            if (cookieToken.Options.Secure)
                builder.Append("Secure; ");

            if (cookieToken.Options.HttpOnly)
                builder.Append("HttpOnly; ");
            if (cookieToken.Options.IsEssential)
                builder.Append("IsEssential; ");

            builder.Append($"SameSite={cookieToken.Options.SameSite}");

            // Append extensions if any
            if (cookieToken.Options.Extensions?.Any() == true)
            {
                builder.Append("; ");  // add separator before extensions
                builder.Append(string.Join("; ", cookieToken.Options.Extensions));
            }
            return builder.ToString();
        }
    }
}
