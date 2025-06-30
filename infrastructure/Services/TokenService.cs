
using Application.Common.Exceptions.JWT;
using Domain.Entities;
using Domain.Entities.Jwt;
using Domain.Interfaces.Services;
using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;


        public TokenService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public string GenerateJwtToken(User user)
        {
            var jwtToken = GenerateJwtAccessToken(user, new List<Claim>());
            return jwtToken.Value;
        }
        public string GenerateJwtToken(User user, IEnumerable<Claim> customClaims)
        {


            string secretKey = _jwtOptions.Secret;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),      //Subject (whom the token refers to)
                    new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                    new Claim("email_Verified", user.Email.IsVerified.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())//unique identifier for this toke
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }


        public JwtAccessToken GenerateJwtAccessToken(User user)
        {
            
            return GenerateJwtAccessToken(user, new List<Claim>());



        }
        public JwtAccessToken GenerateJwtAccessToken(User user, IEnumerable<Claim> customClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
            Guid jti = Guid.NewGuid();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),     //Subject (whom the token refers to)
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                new Claim("email_verified", user.Email.IsVerified.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,jti.ToString() )//unique identifier for this toke
            };



            claims.AddRange(customClaims);

         

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds);

            string tokenstr = new JwtSecurityTokenHandler().WriteToken(token);

            var jwtTokenResult = JwtAccessToken.Create(tokenstr, user.Id, expiresAt, _jwtOptions.Issuer, _jwtOptions.Audience,
                DateTime.UtcNow, claims, jti); 
            if(!jwtTokenResult.IsSuccess)
            {
                throw new TokenGenerationException(jwtTokenResult.Error);
            }

            return jwtTokenResult.Value;





        }

        public RefreshToken GenerateRefreshToken(Guid userId, DeviceFingerprint fingerprint,IpAddress ipAddress, 
                    Guid jwtid,bool isRevoked=false, bool isUsed=false)
        {
            
            DateTime expiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays);

            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            string tokenString = Convert.ToBase64String(randomBytes);
            var tokenResult = RefreshToken.Create(userId, tokenString, expiresAt,fingerprint,ipAddress,jwtid,isRevoked,isUsed);
            if(!tokenResult.IsSuccess)
            {
                throw new TokenGenerationException(tokenResult.Error);
            }
            return tokenResult.Value;
        }

       
        public bool IsJwtTokenValid(JwtAccessToken accessToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _jwtOptions.Issuer,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero   // don't allow clock skew
            };
            return TokenService.IsJwtTokenValid(accessToken, validationParameters);


        }
        public static bool IsJwtTokenValid(JwtAccessToken token, TokenValidationParameters validationParameters)
        {

            try
            {

                new JwtSecurityTokenHandler().ValidateToken(token.Value, validationParameters, out _);
                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidJwtTokenException(ex.ToString());

            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateLifetime = false // We want expired tokens


            };
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;

        }

        public string GetUserIdFromToken(JwtAccessToken token)
        {
            return GetPrincipalFromExpiredToken(token.Value)
                .FindFirstValue(JwtRegisteredClaimNames.Sub)??string.Empty;
        }








            

    }
}
