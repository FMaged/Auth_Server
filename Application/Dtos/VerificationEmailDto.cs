

namespace Application.Dtos
{
    public sealed record VerificationEmailDto(
        string EmailAddress,
        string RecipientName,
        string VerificationLink,
        DateTime LinkExpiration);
}
