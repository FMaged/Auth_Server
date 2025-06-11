
using Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Domain.Validators
{
    public class EmailValidator : IValidator<string>
    {
        private const int MaxLength = 320;
        private const string LocalPartAllowedPattern = @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*$";
        public const string DomainAllowedPattern = @"^[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$";

        public EmailValidator() { }
        public bool Validate(string value, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(value))
                errors.Add("Email cannot be empty.");


            if (value.Length > MaxLength)
                errors.Add($"Email cannot exceed {MaxLength} characters.");

            if (!IsValidFormat(value))
                errors.Add("Email format is invalid.");


            return !errors.Any();
        }



        private static bool IsValidFormat(string email)
        {
            try
            {
                string[] parts = email.Split('@');
                if (parts.Length != 2) return false;

                string localPart = parts[0];
                string domain = parts[1];

                return ValidateLocalPart(localPart) && ValidateDomain(domain);
            }
            catch
            {
                return false;
            }
        }


        private static bool ValidateLocalPart(string localPart)
        {
            if (localPart.Length == 0 || localPart.Length > 64)
                return false;

            if (localPart.StartsWith(".") || localPart.EndsWith("."))
                return false;

            if (Regex.IsMatch(localPart, @"\.\."))
                return false;

            return Regex.IsMatch(localPart, LocalPartAllowedPattern);
        }
        private static bool ValidateDomain(string domain)
        {
            if (domain.Length < 3 || domain.Length > 255)
                return false;

            if (domain.StartsWith(".") || domain.EndsWith("."))
                return false;

            return Regex.IsMatch(domain, DomainAllowedPattern);
        }


    }
}
