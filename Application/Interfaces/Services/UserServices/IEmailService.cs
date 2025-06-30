using Domain.Entities;

namespace Application.Interfaces.Services.UserServices
{
    public interface IEmailService
    {
        //maybe create a Dto for the email content to avoid passing multiple parameters
        // Every email should have a Email, UserName,support link, and other relevant information

        // Core authentication flows
        Task SendAccountDeletionAlertAsync(string email, string userName);
        Task SendPasswordChangedAlertAsync(string email, string userName);
        Task SendPasswordResetAsync(string email, string resetLink);

        // Security notifications
        Task SendTwoFactorCodeEmailAsync(string email, string code);
        Task SendAccountLockoutAlertAsync(string email, DateTime lockoutEnd);
        Task SendNewDeviceLoginAlertAsync(string email, string deviceInfo, DateTime timestamp);


        // Account management
        Task SendEmailVerificationAsync(string email, string verificationLink);
        Task SendEmailChangeAlertAsync(string oldEmail, string SupportLink);
        Task SendUserNameChangeAlert(string oldEmail, string SupportLink);
        Task SendWelcomeEmailAsync(string email, string username);
    }
}
