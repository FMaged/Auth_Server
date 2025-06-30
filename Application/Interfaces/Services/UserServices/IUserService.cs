using Application.Dtos.Requests;
using Domain.Entities;

namespace Application.Interfaces.Services.UserServices
{
    public interface IUserService
    {

        Task<User> RegisterUserAsync(RegisterUserRequestDto userDto);
        Task<User> GetUserByIdAsync(Guid id);
        Task<bool> UpdateUserEmailAsync(UpdateUserEmailRequestDto userDto);
        Task<bool> UpdateUserNameAsync(UpdateUserNameRequestDto userDto);
        Task<bool> ResetUserPasswordAsync(ResetPasswordRequestDto userDto);
        Task ForgotPasswordAsync(ForgotPasswordRequestDto userDto);
        Task ConfirmEmail(Guid userId);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
