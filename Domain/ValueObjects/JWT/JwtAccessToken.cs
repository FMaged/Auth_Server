using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.JWT
{
    public sealed class JwtAccessToken : ValueObject
    {
        private const string JwtPattern = @"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]*$";
        public string Value { get; }
        public Guid Subject { get; } // User ID
        public Guid JWTId { get; }  // Unique identifier for the JWT
        public DateTime Expiry { get; }
        public string Issuer { get; }
        public string Audience { get; }
        public DateTime IssuedAt { get; }
        public IReadOnlyDictionary<string, string> Claims { get; }
        private JwtAccessToken(string value, Guid subject, Guid jWTId, DateTime expiry, string issuer,
                                string audience,
                                DateTime issuedAt,
                                IReadOnlyDictionary<string, string> claims)
        {
            Value = value;
            Subject = subject;
            JWTId = jWTId;
            Expiry = expiry;
            Issuer = issuer;
            Audience = audience;
            IssuedAt = issuedAt;
            Claims = claims;

        }




        public static Result<JwtAccessToken> Create(string value, Guid subject, DateTime expiry,
                                string issuer,
                                string audience,
                                DateTime issuedAt,
                                IReadOnlyDictionary<string, string> claims,Guid jwtId)
        {
            if (string.IsNullOrEmpty(value))
                return Result<JwtAccessToken>.Failure("Token cannot be empty");
            if (value.Split('.').Length != 3)
                return Result<JwtAccessToken>.Failure("Invalid token format");
            if (!Regex.IsMatch(value, JwtPattern))
                return Result<JwtAccessToken>.Failure("Invalid token format");
            if (subject == Guid.Empty)
                return Result<JwtAccessToken>.Failure("Subject cannot be empty");
            if (expiry < DateTime.UtcNow.AddMinutes(1))
                return Result<JwtAccessToken>.Failure("Token expiry too short");
            if (string.IsNullOrEmpty(issuer))
                return Result<JwtAccessToken>.Failure("Issuer cannot be empty");
            if (string.IsNullOrEmpty(audience))
                return Result<JwtAccessToken>.Failure("Audience cannot be empty");
            if (claims == null)
                return Result<JwtAccessToken>.Failure("Claims cannot be null");
            if (issuedAt > DateTime.UtcNow)
                return Result<JwtAccessToken>.Failure("IssuedAt cannot be in the future");
       
            
            return Result<JwtAccessToken>.Success(new JwtAccessToken(value, subject, jwtId, expiry, issuer, audience, issuedAt, claims));

        }
        public bool IsExpired() => DateTime.UtcNow >= Expiry;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Subject;
            yield return JWTId;
            yield return Expiry;
            yield return Issuer;
            yield return Audience;
            yield return IssuedAt;
            yield return Claims;
            
        }
    }
}
