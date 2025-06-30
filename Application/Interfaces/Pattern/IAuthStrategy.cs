

using Application.Dtos;
using Application.Dtos.Requests;

namespace Application.Interfaces.Pattern
{
    public interface IAuthStrategy
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<bool> LogoutAsync(LoginRequest request);

    }


}


