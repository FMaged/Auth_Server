namespace Domain.ValueObjects.User.Helpers
{
    public class PasswordOptions
    {
        public const string Section = "PasswordOptions";
        public int RequiredUniqueChars { get; set; } = 1;
        public bool RequireNonAlphanumeric { get; set; } = true;
        public bool RequireLowercase { get; set;} = true;
        public bool RequireUppercase { get; set;} = true;
        public bool RequireDigit { get; set;} = true;
        public int MinLength { get; set; } = 8;
        public int MaxLength { get; set; } = 128;
        public string AllowedPattern { get; set; } = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).+$";

        public PasswordOptions() 
        {
            //RequiredUniqueChars = default!;
            //RequireNonAlphanumeric = default!;
            //RequireLowercase = default!;
            //RequireUppercase = default!;
            //RequireDigit = default!;
            //MinLength = default!;
            //MaxLength = default!;
            //AllowedPattern = default!;

        }

        public PasswordOptions(int requiredUniqueChars, bool requireNonAlphanumeric,
                                bool requireLowercase, bool requireUppercase,
                                bool requireDigit, int minLength, int maxLength, string allowedPattern)
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
