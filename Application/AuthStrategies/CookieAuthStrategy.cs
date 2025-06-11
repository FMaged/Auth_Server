

using Application.Dtos.Requests;
using Application.Interfaces.Pattern;
using Application.UseCases.Authentication.Commands.Login;

namespace Application.AuthStrategies
{
    internal class CookieAuthStrategy : IAuthStrategy
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> LogoutAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
