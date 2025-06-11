

using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User
{
    public class UserName:ValueObject
    {
        public string Value { get; }
        private const int MinLength = 3;
        private const int MaxLength = 20;
        private const string AllowedCharactersPattern = @"^[\w-]+$";
        private UserName(string value) => Value = value;
        public static Result<UserName> Create(string rawUserName)
        {
            if (string.IsNullOrWhiteSpace(rawUserName))
                return Result<UserName>.Failure("Username cannot be empty.");
            string trimmedUserName = rawUserName.Trim();
            if (trimmedUserName.Length < MinLength || trimmedUserName.Length > MaxLength)
                return Result<UserName>.Failure($"Username must be between {MinLength} and {MaxLength} characters long.");
            if (!Regex.IsMatch(trimmedUserName, AllowedCharactersPattern))
                return Result<UserName>.Failure("Username contains invalid characters.");
            return Result<UserName>.Success(new UserName(trimmedUserName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
