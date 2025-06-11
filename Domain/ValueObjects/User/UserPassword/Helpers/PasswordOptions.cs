namespace Domain.ValueObjects.User.UserPassword.Helpers
{
    public sealed class PasswordOptions
    {

        public int RequiredUniqueChars { get; } = 1;
        public bool RequireNonAlphanumeric { get; } = true;
        public bool RequireLowercase { get; } = true;
        public bool RequireUppercase { get; } = true;
        public bool RequireDigit { get; } = true;
        public int MinLength { get; } = 8;
        public int MaxLength { get; } = 128;
        public string AllowedPattern { get; } = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$";

        public PasswordOptions() { }
        public PasswordOptions(int requiredUniqueChars, bool requireNonAlphanumeric, bool requireLowercase,
            bool requireUppercase, bool requireDigit, int minLength, int maxLength, string allowedPattern)
        {
            RequiredUniqueChars = requiredUniqueChars;
            RequireNonAlphanumeric = requireNonAlphanumeric;
            RequireLowercase = requireLowercase;
            RequireUppercase = requireUppercase;
            RequireDigit = requireDigit;
            MinLength = minLength;
            MaxLength = maxLength;
            AllowedPattern = allowedPattern;
        }



    }
}
