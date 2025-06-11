using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IEmailService
    {
        // Core authentication flows
        Task SendEmailVerificationAsync(string email, string verificationLink);
        Task SendPasswordResetEmailAsync(string email, string resetLink);
        Task SendTwoFactorCodeEmailAsync(string email, string code);

        // Security notifications
        Task SendAccountLockoutAlertAsync(string email, DateTime lockoutEnd);
        Task SendNewDeviceLoginAlertAsync(string email, string deviceInfo, DateTime timestamp);
        Task SendPasswordChangedAlertAsync(string email);

        // Account management
        Task SendEmailChangeConfirmationAsync(string newEmail, string confirmationLink);
        Task SendWelcomeEmailAsync(string email, string username);
    }
}
