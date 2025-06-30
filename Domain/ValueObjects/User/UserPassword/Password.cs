
using Domain.Interfaces.Services;
using Domain.Shared;
using Domain.Validators;
using Domain.ValueObjects.User.Helpers;






namespace Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {

        public string Value { get; }

        private Password(string value) => Value = value; //allowing it to be Public and changing the Create method from static
                                                                //still nedds the Create() to create the Password
        public static Result<Password> Create(string plainPassword, IValidator<string> validator)
        {
            
            var trimmedPassword = plainPassword.Trim();
            if (!validator.Validate(trimmedPassword, out var errors))
            {
                return Result<Password>.Failure(string.Join(",\n", errors));
            }
            if(IsCommonPassword(trimmedPassword))
            {
                return Result<Password>.Failure("Password is too common, please choose a more secure password.");
            }
            return Result<Password>.Success(new Password(trimmedPassword));

        }


        private static bool IsCommonPassword(string password)
        {
            // This is a simplified example. I can use a Hashset or a database of common passwords.
            var commonPasswords = new[] { "123456", "password", "123456789", "12345678", "12345" };
            return commonPasswords.Contains(password);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;

        }
    }

}
