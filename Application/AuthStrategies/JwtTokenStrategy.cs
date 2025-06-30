using Application.Common.Exceptions.JWT;
using Application.Common.Exceptions.User;
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Interfaces.Pattern;
using Domain.Entities;
using Domain.Entities.Jwt;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.ValueObjects;
using Domain.ValueObjects.User;
using Domain.ValueObjects.User.Helpers;

namespace Application.AuthStrategies
{
    public class JwtTokenStrategy: IAuthStrategy
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthSessionRepository _jwtAuthSessionRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;
        private readonly PasswordOptions _passwordOptions;


        public JwtTokenStrategy(
            IUserRepository userRepository,
            IJwtAuthSessionRepository jwtAuthSessionRepository,
            ITokenService tokenService,
            IHashService hashService,
            PasswordOptions options)
        {
            _userRepository = userRepository;
            _jwtAuthSessionRepository = jwtAuthSessionRepository;
            _tokenService = tokenService;
            _hashService = hashService;
            _passwordOptions = options;
        }   

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await GetUserAsync(request);
            if (user is null)
            {
                throw new UserNotFoundException("User not found with the provided credentials.");
            }

            var passwordResult=Password.Create("MKShajshs,2n",_passwordOptions);
            if (!passwordResult.IsSuccess)
            {
                throw new PasswordException(passwordResult.Error);
            }
            if(!_hashService.Verify(passwordResult.Value, user.PasswordHash))
            {
                throw new PasswordException("Invalid password.");
            }
            var accessToken = _tokenService.GenerateJwtAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id,user.DeviceFingerprint,user.IpAddress,accessToken.JWTId);
            var jwtAuthSession=JwtAuthSession.Create(
                user.Id,
                accessToken,
                refreshToken,
                user.DeviceFingerprint,
                user.IpAddress
            );
            if(!jwtAuthSession.IsSuccess)
            {
                throw new InvalidJwtSessionException(jwtAuthSession.Error);

            }
            if (await _jwtAuthSessionRepository.AddSessionAsync(jwtAuthSession.Value) == false)
            {
                throw new InvalidJwtSessionException("Saving the session failed");
            }
            AuthResponse response = new AuthResponse(accessToken, refreshToken, null);
            return response;

            


        }
        public async Task<bool> LogoutAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                throw new InvalidJwtSessionException("Refresh token is required for logout.");
            }
            var session = await _jwtAuthSessionRepository.GetSessionByRefreshTokenAsync(request.RefreshToken);
            if(session is null)
            {
                throw new InvalidJwtSessionException("Session not found for the provided refresh token.");
            }
            bool deleted = await _jwtAuthSessionRepository.DeleteSessionAsync(session.Id);
            if (!deleted)
            {
                throw new InvalidJwtSessionException("Failed to delete the session.");
            }
            return deleted;

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
