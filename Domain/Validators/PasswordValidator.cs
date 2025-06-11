
using Domain.Interfaces.Services;
using Domain.ValueObjects.User.UserPassword.Helpers;
using System.Text.RegularExpressions;

namespace Domain.Validators
{
    public class PasswordValidator : IValidator<string>
    {
        private readonly PasswordOptions _options;
        public PasswordValidator(PasswordOptions options)
        {
            _options = options;
        }


        public bool Validate(string value, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(value) || value.Length < _options.MinLength)
                errors.Add($"Password must be at least {_options.MinLength} characters.");

            if(value.Length > _options.MaxLength)
                errors.Add($"Password cannot exceed {_options.MaxLength} characters.");

            if (_options.RequireDigit && !value.Any(char.IsDigit))
                errors.Add("Password must contain at least one digit.");

            if (_options.RequireUppercase && !value.Any(char.IsUpper))
                errors.Add("Password must contain at least one uppercase letter.");

            if (_options.RequireLowercase && !value.Any(char.IsLower))
                errors.Add("Password must contain at least one lowercase letter.");

            if (_options.RequireNonAlphanumeric && !value.Any(ch => !char.IsLetterOrDigit(ch)))
                errors.Add("Password must contain at least one non-alphanumeric character.");

            if (_options.RequiredUniqueChars > 0 && value.Distinct().Count() < _options.RequiredUniqueChars)
                errors.Add($"Password must contain at least {_options.RequiredUniqueChars} unique characters.");


            // Validate against the allowed pattern
            if (!Regex.IsMatch(value, _options.AllowedPattern))
                errors.Add("Password does not match the required pattern.");

            return !errors.Any();





        }
    }
}
