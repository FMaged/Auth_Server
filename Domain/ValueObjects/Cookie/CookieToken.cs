using Domain.Enums; 
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Cookie
{
    public sealed class CookieToken : ValueObject
    {
        public string Name { get; }
        public string SessionId { get; }

        public CookieOptions Options { get; }// DI needs to handle the static method







        private CookieToken(string name, string value, CookieOptions options)
        {
            Name = name;
            SessionId = value;
            Options = options;
        }



        public static Result<CookieToken> Create( string value, CookieOptions options, string name="asSession")
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Cookie name cannot be empty.");
            if (!Regex.IsMatch(name, CookieHelpers.AllowedNamePattern))
                errors.Add("Invalid cookie name format");
            if (string.IsNullOrWhiteSpace(value))
                errors.Add("Cookie value cannot be empty.");
            if (value.Length > CookieHelpers.MaxValueLength)
                errors.Add("Cookie value exceeds maximum length");
            if (options.SameSite == SameSiteType.None && !options.Secure)
                errors.Add("Secure flag required for SameSite=None");
            if (options.Expires <= DateTime.UtcNow.AddMinutes(1))
                errors.Add("Expiration date must be at least 1 minute in the future.");
            if (!string.IsNullOrWhiteSpace(options.Domain) && !Regex.IsMatch(options.Domain, CookieHelpers.AllowedDomainPattern))
                errors.Add("Invalid domain format.");

            if (errors.Any())
                return Result<CookieToken>.Failure(string.Join(" ", errors));
            return Result<CookieToken>.Success(new CookieToken(name, value,options));
        }













        public bool IsExpired() => DateTime.UtcNow >= Options.Expires;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return SessionId;
            yield return Options;

        }
    }
}
