

using Application.Interfaces.Pattern;
using Domain.Enums;

namespace Application.AuthStrategies
{
    public class AuthStrategyFactory : IAuthStrategyFactory
    {
        private readonly JwtTokenStrategy _jwtStrategy;
        private readonly OAuthStrategy _oauthStrategy;
        private readonly CookieAuthStrategy _cookieStrategy;

        public AuthStrategyFactory(
            JwtTokenStrategy jwtStrategy,
            OAuthStrategy oauthStrategy,
            CookieAuthStrategy cookieStrategy)
        {
            _jwtStrategy = jwtStrategy;
            _oauthStrategy = oauthStrategy;
            _cookieStrategy = cookieStrategy;
        }



        public IAuthStrategy CreateAuthStrategy(string type)
        {
            ClientType clientType = CreateEnumType(type);
            return clientType switch
            {
                ClientType.Web => _cookieStrategy,
                ClientType.Mobile => _jwtStrategy,
                ClientType.Desktop => _jwtStrategy,
                ClientType.Api => _oauthStrategy,
                _ => throw new ArgumentException("Invalid authentication strategy type", nameof(type))
            };

        }




        private ClientType CreateEnumType(string type)
        {
            return type switch
            {
                "Web" => ClientType.Web,
                "Mobile" => ClientType.Mobile,
                "Desktop" => ClientType.Desktop,
                "Api" => ClientType.Api,
                _ => ClientType.Other
            };
        }
}
}
