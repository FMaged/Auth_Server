using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.OAuth
{

    public sealed class OAuthCode : ValueObject
    {
        // OAuth 2.0 RFC 6749 requirements
        private const int MinLength = 32;
        private const int MaxLength = 256;
        private const string ValidCharactersPattern = @"^[a-zA-Z0-9\-._~+/]+$";
        public string Value { get; }
        public string CodeVerifier { get; } // PKCE extension (RFC 7636)
        public string RedirectUri { get; }
        private OAuthCode(string value, string codeVerifier, string redirectUri)
        {
            Value = value;
            CodeVerifier = codeVerifier;
            RedirectUri = redirectUri;
        }
        public static Result<OAuthCode> Create(
            string code,
            string codeVerifier,
            string redirectUri,
            bool validatePkce = true)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Result<OAuthCode>.Failure("Authorization code cannot be empty");
            if (code.Length < MinLength || code.Length > MaxLength)
                return Result<OAuthCode>.Failure($"Code must be between {MinLength}-{MaxLength} chars");
            if (!Regex.IsMatch(code, ValidCharactersPattern))
                return Result<OAuthCode>.Failure("Code contains invalid characters");
            // PKCE validation
            if (validatePkce)
            {
                if (string.IsNullOrWhiteSpace(codeVerifier))
                    return Result<OAuthCode>.Failure("PKCE code verifier required");

                if (codeVerifier.Length < 43 || codeVerifier.Length > 128)
                    return Result<OAuthCode>.Failure("Code verifier must be 43-128 chars");
            }
            // Redirect URI validation
            if (string.IsNullOrWhiteSpace(redirectUri))
                return Result<OAuthCode>.Failure("Redirect URI required");

            if (!Uri.IsWellFormedUriString(redirectUri, UriKind.Absolute))
                return Result<OAuthCode>.Failure("Invalid redirect URI format");

            return Result<OAuthCode>.Success(new OAuthCode(
                value: code.Trim(),
                codeVerifier: codeVerifier?.Trim(),
                redirectUri: redirectUri.Trim()));

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;

        }


        // needs to Move
        public record PkceData(string CodeVerifier, string CodeChallenge);
        public static PkceData GeneratePkceData()
        {
            var codeVerifier = GenerateRandomString(128);
            var codeChallenge = GenerateCodeChallenge(codeVerifier);
            return new PkceData(codeVerifier, codeChallenge);
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~+/=";
            var random = new Random();
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }
        private static string GenerateCodeChallenge(string codeVerifier)
        {
            // SHA256 hash the code verifier and base64url encode it
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(codeVerifier));
                return Convert.ToBase64String(hash)
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');
            }
        }

    }
}
