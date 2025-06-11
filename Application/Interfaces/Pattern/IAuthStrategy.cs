

using Application.Dtos.Requests;
using Application.UseCases.Authentication.Commands.Login;

namespace Application.Interfaces.Pattern
{
    public interface IAuthStrategy
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> LogoutAsync(LoginRequest request);

    }


}


