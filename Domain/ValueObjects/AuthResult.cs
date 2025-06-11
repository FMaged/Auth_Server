using Domain.Entities.Jwt;
using Domain.Shared;
using Domain.ValueObjects.Cookie;
using Domain.ValueObjects.JWT;

namespace Domain.ValueObjects
{
    public class AuthResult:ValueObject
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public JwtAccessToken? JwtToken { get; set; }  
        public CookieToken? CookieToken { get; set; } 
        public RefreshToken? RefreshToken { get; set; }
        
        private AuthResult(bool success, string? errorMassage, JwtAccessToken? jwtAccessToken,
                            CookieToken? cookieToken,RefreshToken? refreshToken) 
        {
            Success = success;
            ErrorMessage = errorMassage;
            JwtToken = jwtAccessToken;
            CookieToken = cookieToken;
            RefreshToken = refreshToken;






        }

        public static Result<AuthResult> Create(bool success, string? errorMassage, JwtAccessToken? jwtAccessToken,
                            CookieToken? cookieToken, RefreshToken? refreshToken)
        {
            if (!success && string.IsNullOrWhiteSpace(errorMassage))
                return Result<AuthResult>.Failure("Error message must be provided if not successful.");
            
            var authResult = new AuthResult(success, errorMassage, jwtAccessToken, cookieToken, refreshToken);
            return Result<AuthResult>.Success(authResult);
        }
        public static Result<AuthResult> CreateJWT(JwtAccessToken jwtAccessToken, RefreshToken refreshToken)
        {

            return Result<AuthResult>.Success(
                new AuthResult(true, null, jwtAccessToken, null, refreshToken));
        }
        public static Result<AuthResult> CreateCookie(CookieToken cookieToken)
        {


            return Result<AuthResult>.Success(new AuthResult(true, null, null, cookieToken, null));

        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Success;
            yield return ErrorMessage ?? string.Empty;
            yield return JwtToken?.Value ?? string.Empty;
            yield return CookieToken?.Value ?? string.Empty;
            yield return RefreshToken?.UserId.ToString()?? string.Empty;



        }
    }
}
