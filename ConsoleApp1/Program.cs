using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Shared;
using Domain.ValueObjects;
using Domain.ValueObjects.Cookie;
using Domain.ValueObjects.JWT;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.Helpers;
using Domain.ValueObjects.User.UserPassword;
using infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{

    class Program
    {


        static void Main(string[] args)
        {



            var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .AddUserSecrets<Program>() 
        .Build();

        // Set up DI
        var services = new ServiceCollection();
        services.AddServices(config); // uses  DependencyInjection.AddInfrastructure()
        services.AddOptions(config);
        // Build provider
        var provider = services.BuildServiceProvider();



        var cookieOptions = provider.GetService<CookieOptions>();
            if (cookieOptions != null)
            {
                Console.WriteLine("✅ JwtOptions loaded:");

            }

            // Resolve JwtOptions using IOptions<T> returns null °!!!!
            var jwtOptions = provider.GetService< JwtOptions>();


        if (jwtOptions != null) 
        {
            Console.WriteLine("✅ JwtOptions loaded:");
            Console.WriteLine($"RefreshTokenLifetimeDays: {jwtOptions.RefreshTokenLifetimeDays}");
            Console.WriteLine($"ExpirationMinutes: {jwtOptions.ExpirationMinutes}");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve JwtOptions.");
        }


        // Resolve options directly
        var options = provider.GetService<HashingOptions>();
        if (options != null)
        {
            Console.WriteLine("✅ HashingOptions loaded:");
            Console.WriteLine($"SaltSize: {options.SaltSize}");
            Console.WriteLine($"MemorySize: {options.MemorySize}");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve HashingOptions.");
        }

        var IuserRepository = provider.GetService<IUserRepository>();
        if(IuserRepository != null)
        {
            Console.WriteLine("✅ IUserRepository resolved successfully.");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve IUserRepository.");
        }
        var IRefreshRepository = provider.GetService<IRefreshTokenRepository>();
        if (IRefreshRepository != null)
        {
            Console.WriteLine("✅ IRefreshTokenRepository resolved successfully.");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve IRefreshTokenRepository.");
        }


        var cookieService = provider.GetService<ICookieService>();
        if (cookieService != null)
        {
            Console.WriteLine("✅ cookieService resolved successfully.");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve cookieService.");
        }
            // Resolve  IHashService
            var hashService = provider.GetService<IHashService>();

        // Resolve ITokenService
        var tokenService = provider.GetService<ITokenService>();

        if (tokenService != null)
        {
            Console.WriteLine("✅ TokenService resolved successfully.");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve TokenService.");
        }


        if (hashService != null)
        {
            Console.WriteLine("✅ HashService resolved successfully.");
        }
        else
        {
            Console.WriteLine("❌ Failed to resolve HashService.");
        }



            var cookietoken = cookieService.GenerateSecureToken();
            var cookieSession = cookieService.BuildCookieHeader(CookieToken.Create(cookietoken, cookieOptions).Value);
            var cookieToken = cookieService.ParseCookieHeader(cookieSession);


        Result<Email> emailResult = Email.Create("zebo@wmsk.com");
        if(!emailResult.IsSuccess)
        {
            Console.WriteLine($"Error: {emailResult.Error}");
            return;
        }
        Email email = emailResult.Value;


        var usernameResult = UserName.Create("MAHdjakm25");
        if (!usernameResult.IsSuccess)
        {
            Console.WriteLine($"Error: {usernameResult.Error}");
            return;
        }
        UserName userName = usernameResult.Value;

        var PhoneResult = PhoneNumber.Create("+49", "15583298171");
        if (!PhoneResult.IsSuccess)
        {
            Console.WriteLine($"Error: {PhoneResult.Error}");
            return;
        }
        PhoneNumber phoneNumber = PhoneResult.Value;



        var Result = IpAddress.Create("127.0.0.1");
        if (!Result.IsSuccess)
            Console.WriteLine($"Error: {Result.Error}");
        IpAddress ip = Result.Value;

        //HashingOptions hashingOptions = new HashingOptions();
        //HashService hashService = new HashService(hashingOptions);




        var PasswordResult = Password.Create("MKShajshs,2n", new PasswordOptions());
        if (!PasswordResult.IsSuccess)
            Console.WriteLine($"Error: {PasswordResult.Error}");
        Password password = PasswordResult.Value;
        HashedPassword hashedPassword = hashService.HashPassword(password);
            
        string finger = "5+bt5WjR4UCd+JaTTt30eA==";
        var fingerprintResult = DeviceFingerprint.Create( hashService.Hash(finger));
        if (!fingerprintResult.IsSuccess)
            Console.WriteLine($"Error: {fingerprintResult.Error}");
        DeviceFingerprint fingerprint = fingerprintResult.Value;

        UserTimestamps timestamps=new(DateTime.UtcNow,null,null,null,null,null);
        UserSecurityData securityData = new(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), false, 0);

        User user = User.CreateO(Guid.NewGuid(), email, hashedPassword, userName, true, timestamps, fingerprint,ip, phoneNumber, securityData).Value;

        JwtAccessToken token = tokenService.GenerateJwtAccessToken(user);

        DateTime expiresAt = DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenLifetimeDays);
        //var refreshToken = tokenService.GenerateRefreshToken(user.Id, expiresAt, fingerprint, ip, token.JWTId,false,false);
             

    }
}
}
