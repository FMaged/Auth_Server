

using Microsoft.Extensions.Options;

namespace Domain.ValueObjects.JWT
{
    public class JwtOptions
    {
        //this has to match the JwtOptions in appsettings.json
        public const string Section = "JwtOptions";
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
        public int RefreshTokenLifetimeDays { get; set;  }

        public JwtOptions() 
        {
            Secret=default!;
            Issuer = default!;
            Audience = default!;
            ExpirationMinutes = default!;
            RefreshTokenLifetimeDays = default!;

        }
        public JwtOptions(string secret,string issuer, string audience, int expirationMinutes, 
                int refreshTokenLifetimeDays)
        {
            Secret = secret;
            Issuer = issuer;
            Audience = audience;
            ExpirationMinutes = expirationMinutes;
            RefreshTokenLifetimeDays = refreshTokenLifetimeDays;
        }


        





    }
}
