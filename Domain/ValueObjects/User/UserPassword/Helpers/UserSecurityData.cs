namespace Domain.ValueObjects.User.UserPassword.Helpers
{
    public record UserSecurityData(
        string SecurityStamp,
        string ConcurrencyStamp,
        bool TwoFactorEnabled,
        int FailedLoginAttempts
    );
}
