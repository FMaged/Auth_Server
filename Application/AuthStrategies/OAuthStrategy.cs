
using Application.Dtos;
using Application.Dtos.Requests;
using Application.Interfaces.Pattern;

namespace Application.AuthStrategies
{
    public class OAuthStrategy : IAuthStrategy
    {
        public Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }


}
