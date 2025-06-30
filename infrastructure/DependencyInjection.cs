
using Application.AuthStrategies;
using Application.Interfaces.Pattern;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Validators;
using Domain.ValueObjects.Cookie;
using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User.Helpers;
using infrastructure.Repository;
using infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordOptions>(configuration.GetSection(PasswordOptions.Section));
            services.Configure<HashingOptions>(configuration.GetSection(HashingOptions.Section));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Section));
            services.Configure<CookieOptions>(configuration.GetSection(CookieOptions.Section));
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<PasswordOptions>>().Value);
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<HashingOptions>>().Value);
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<JwtOptions>>().Value);
            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<CookieOptions>>().Value);

            
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            //AddSingleton =Create one single instance of HashService and reuse it everywhere.
            services.AddSingleton<IHashService, HashService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICookieService, CookieService>();
            
            services.AddSingleton<IValidator<string>,PasswordValidator>();  // this return PasswordValidator needs tho be changed
                                                                            // IValidator should be generic
            services.AddSingleton<JwtTokenStrategy>();
            services.AddSingleton<CookieAuthStrategy>();
            services.AddSingleton<OAuthStrategy>();
            services.AddSingleton<IAuthStrategyFactory,AuthStrategyFactory>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<IJwtAuthSessionRepository, JwtAuthSessionRepository>();
            services.AddSingleton<ICookieSessionRepository, CookieSessionRepository>();
            return services;
        }

    }
}
