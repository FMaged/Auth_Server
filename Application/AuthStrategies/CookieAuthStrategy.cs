using Application.Common.Exceptions.CookieSession;
using Application.Common.Exceptions.User;
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Interfaces.Pattern;
using Domain.Entities;
using Domain.Entities.Cookie;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Validators;
using Domain.ValueObjects;
using Domain.ValueObjects.Cookie;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.Helpers;

namespace Application.AuthStrategies
{
    public class CookieAuthStrategy : IAuthStrategy
    {
        private readonly IUserRepository _userRepository;
        private readonly ICookieSessionRepository _cookieSessionRepository;
        private readonly IHashService _hashService;
        private readonly ICookieService _cookieService;
        private readonly PasswordValidator validator;


        public CookieAuthStrategy(
            IUserRepository userRepository,
            ICookieSessionRepository cookieSessionRepository,
            IHashService hashService,
            ICookieService cookieService)
        {
            _userRepository = userRepository;
            _cookieSessionRepository = cookieSessionRepository;
            _hashService = hashService;
            _cookieService = cookieService;
        }



        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await GetUserAsync(request);
            if (user is null)
            {
                throw new UserNotFoundException("User not found with the provided credentials.");
            }
            var passwordResult = Password.Create(request.Password, new PasswordOptions());
            if (!passwordResult.IsSuccess)
            {
                throw new PasswordException(passwordResult.Error);
            }
            if (!_hashService.Verify(passwordResult.Value, user.PasswordHash))
            {
                throw new PasswordException("Invalid password.");
            }
            var Token = _cookieService.GenerateSecureToken();
            var CookieTokenResult = CookieToken.Create(Token,new CookieOptions());
            if (!CookieTokenResult.IsSuccess)
            {
                throw new InvalidCookiesException(CookieTokenResult.Error);
            }
            var cookieResultSession = CookieAuthSession.Create(user.Id, CookieTokenResult.Value, user.DeviceFingerprint, user.IpAddress);
            if (!cookieResultSession.IsSuccess)
            {
                throw new InvalidCookieSessionException(cookieResultSession.Error);
            }
            if (await _cookieSessionRepository.AddSessionAsync(cookieResultSession.Value) == false)
            {
                throw new InvalidCookieSessionException("Failed to create cookie session.");
            }
            string cookieString = _cookieService.BuildCookieHeader(CookieTokenResult.Value);
            AuthResponse response = new AuthResponse(null, null, cookieString);
            return response;


        }

        public async Task<bool> LogoutAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CookieHeader))
            {
                throw new InvalidCookiesException("Cookie header is required for logout.");
            }
            CookieToken cookie = _cookieService.ParseCookieHeader(request.CookieHeader);

            CookieAuthSession? authSession = await _cookieSessionRepository.GetSessionByIdAsync(cookie.SessionId);
            if (authSession is null)
            {
                throw new InvalidCookieSessionException("No active session found for the provided cookie.");
            }
            bool deleted = await _cookieSessionRepository.DeleteSessionAsync(cookie.SessionId);
            if (!deleted)
            {
                throw new InvalidCookieSessionException("Failed to delete the session.");
            }
            return true;

        }

        private async Task<User?> GetUserAsync(LoginRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var emailResult = Email.Create(request.Email);
                if (!emailResult.IsSuccess)
                    throw new EmailFormatException(emailResult.Error);
                return await _userRepository.GetUserByEmailAsync(emailResult.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                var userNameResult = UserName.Create(request.Username);
                if (!userNameResult.IsSuccess)
                    throw new UserNameFormatException(userNameResult.Error);
                return await _userRepository.GetUserByUserNameAsync(userNameResult.Value);

            }
            return null;
        }

    }
}
