

namespace Application.UseCases.PasswordManagement
{
   
    public record UpdatePasswordCommand(
        string UserId,
        string OldPassword,
        string NewPassword);

}