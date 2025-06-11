

namespace Domain.ValueObjects.JWT
{
    public sealed class JwtOptions
    {
        //this has to match the JwtOptions in appsettings.json
        public const string JwtOptionsKey= "JwtOptions";
        public string Secret { get; } 
        public string Issuer { get; }
        public string Audience { get; }
        public int ExpirationMinutes { get; }


        public JwtOptions(string secret, string issuer, string audience, int accessTokenExpirationMinutes)
        {
            Secret = secret;
            Issuer = issuer;
            Audience = audience;
            ExpirationMinutes = accessTokenExpirationMinutes;
        }




    }
}
