using Domain.Enums;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.OAuth
{
    //access token received from Google/GitHub
    public sealed class OAuthAccessToken : ValueObject
    {
        private const string JwtPattern = @"^[a-zA-Z0-9\-._~+/]+$";
        public string Value { get; }
        public DateTime IssuedAt { get; }
        public DateTime ExpiresAt { get; }
        public IReadOnlyDictionary<string, string> Claims { get; }
        public OAuthProvider Provider { get; }
        public TokenType Type { get; }
        private OAuthAccessToken(string value, DateTime issuedAt, DateTime expiresAt,
                            IReadOnlyDictionary<string, string> claims, OAuthProvider provider,
                            TokenType type)
        {
            Value = value;
            IssuedAt = issuedAt;
            ExpiresAt = expiresAt;
            Claims = claims;
            Provider = provider;
            Type = type;
        }
        public static Result<OAuthAccessToken> Create(string value, DateTime issuedAt,DateTime expiresAt,
                        IReadOnlyDictionary<string, string> claims,OAuthProvider provider,
                        TokenType type)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<OAuthAccessToken>.Failure("Access token cannot be empty");
            if (!Regex.IsMatch(value, JwtPattern))
                return Result<OAuthAccessToken>.Failure("Invalid access token format");
            if (issuedAt > expiresAt)
                return Result<OAuthAccessToken>.Failure("Issued at time cannot be after expiration time");
            if (issuedAt < DateTime.UtcNow)
                return Result<OAuthAccessToken>.Failure("Access token is not yet valid");
            if (expiresAt < DateTime.UtcNow)
                return Result<OAuthAccessToken>.Failure("Access token has expired");
            if (claims == null || claims.Count == 0)
                return Result<OAuthAccessToken>.Failure("Claims cannot be empty");
            return Result<OAuthAccessToken>.Success(new OAuthAccessToken(
                value: value.Trim(),
                issuedAt: issuedAt,
                expiresAt: expiresAt,
                claims: claims,
                provider: provider,
                type: type));





        }




        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return IssuedAt;
            yield return ExpiresAt;
            yield return Claims;
            yield return Provider;
            yield return Type;

        }
    }






}
