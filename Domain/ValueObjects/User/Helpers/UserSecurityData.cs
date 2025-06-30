using System.Numerics;
using System.Threading.Channels;

namespace Domain.ValueObjects.User.Helpers
{
    // Summary:

    public record UserSecurityData(

        string SecurityStamp,   //Tracks security-related changes to a user’s account.
                                //Changes when: Password is changed, Email/username is changed, Two-factor authentication is enabled/disabled

        string ConcurrencyStamp,//Tracks any concurrent modifications to the user record.
                                //Detecting conflicts when multiple sources try to update the user at the same time.
        bool TwoFactorEnabled,
        int FailedLoginAttempts
    );
}
