namespace Domain.ValueObjects.User.UserPassword.Helpers
{
    public record UserTimestamps(
        DateTime CreatedAt,
        DateTime? LastLoginAt,
        DateTime? LastPasswordChangeAt,
        DateTime? LastEmailChangeAt,
        DateTime? LastUserNameChangeAt,
        DateTime? EmailConfirmedAt
    );
}
