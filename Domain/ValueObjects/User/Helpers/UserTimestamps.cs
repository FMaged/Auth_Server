namespace Domain.ValueObjects.User.Helpers
{
    public record UserTimestamps(
        DateTime CreatedAt,
        DateTime? LastLoginAt = null,
        DateTime? LastPasswordChangeAt = null,
        DateTime? LastEmailChangeAt = null,
        DateTime? LastUserNameChangeAt = null,
        DateTime? EmailConfirmedAt = null
    );
}
