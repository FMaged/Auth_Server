using Domain.Enums;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Cookie
{
    public sealed class CookieToken : ValueObject
    {
        public string Name { get; }
        public string Value { get; }
        public DateTime Expiration { get; }
        public TimeSpan MaxAge => Expiration - DateTime.UtcNow;

        public SameSiteType SameSite { get; }
        public bool HttpOnly { get; }
        public bool Secure { get; }
        public string? Domain { get; }
        public string? Path { get; }
        public bool IsEssential { get; }

        private List<string>? _extensions;

        /// <summary>
        /// Gets a collection of additional values to append to the cookie.
        /// </summary>
        public IList<string> Extensions
        {
            get => _extensions ??= new List<string>();
        }

        private const string AllowedNamePattern = @"^[a-zA-Z0-9_-]+$";
        private const string AllowedDomainPattern = @"^(\*\.)?[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$";
        private const int MaxValueLength = 4096;
        private const string SeparatorToken = "; ";
        private const string EqualsToken = "=";
        private const int ExpiresDateLength = 29;
        private const string ExpiresDateFormat = "r";


        private CookieToken(string name, string value, DateTime expiration,
                            SameSiteType sameSite, bool httpOnly, bool secure, string domain,
                            string path,bool isEssential, IList<string>? extensions)
        {
            Name = name;
            Value = value;
            Expiration = expiration;
            SameSite = sameSite;
            HttpOnly = httpOnly;
            Secure = secure;
            Domain = domain;
            Path = path;
            IsEssential=isEssential;
            _extensions = extensions?.ToList() ?? new List<string>();
        }

        public static Result<CookieToken> Create(string name, string value, CookieOptions options)
        {
            return Create(name, value, options.Expires,
                            options.SameSite, options.HttpOnly, options.Secure, options.Domain,
                            options.Path, options.IsEssential, options.Extensions);



        }

        public static Result<CookieToken> Create(string name, string value, DateTime expiration,
                            SameSiteType sameSite, bool httpOnly, bool secure, string domain,
                            string path,bool isEssential, IList<string>? extensions)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Cookie name cannot be empty.");
            if (!Regex.IsMatch(name, AllowedNamePattern))
                errors.Add("Invalid cookie name format");
            if (string.IsNullOrWhiteSpace(value))
                errors.Add("Cookie value cannot be empty.");
            if (value.Length > MaxValueLength)
                errors.Add("Cookie value exceeds maximum length");
            if (sameSite == SameSiteType.None && !secure)
                errors.Add("Secure flag required for SameSite=None");
            if (expiration <= DateTime.UtcNow.AddMinutes(1))
                errors.Add("Expiration date must be at least 1 minute in the future.");
            if (!string.IsNullOrWhiteSpace(domain) && !Regex.IsMatch(domain, AllowedDomainPattern))
                errors.Add("Invalid domain format.");

            if (errors.Any())
                return Result<CookieToken>.Failure(string.Join(" ", errors));
            return Result<CookieToken>.Success(new CookieToken(name, value, expiration, sameSite, httpOnly, secure, domain, path, isEssential, extensions));
        }















        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Value;
            yield return Expiration;
            yield return SameSite;
            yield return HttpOnly;
            yield return Secure;
            yield return Domain??string.Empty;
            yield return Path??string.Empty;
            yield return IsEssential;
            yield return _extensions??new List<string>();

        }
    }
}
