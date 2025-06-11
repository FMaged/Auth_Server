

using Domain.Shared;
using Domain.Validators;
using System.Runtime.CompilerServices;

namespace Domain.ValueObjects.User
{
    public sealed class Email:ValueObject
    {
        public string Value { get;}
        public bool IsVerified { get; }

        private Email(string value,bool isVerified=false) 
        {
            Value = value;
            IsVerified = isVerified;
        }
        public static Result<Email> Create(string rawEmail)
        {
            EmailValidator validator=new EmailValidator();
            if(!validator.Validate(rawEmail,out List<string> errors))
                return Result<Email>.Failure(string.Join(",\n", errors));
            string TrimmedEmail = rawEmail.Trim();

            return Result<Email>.Success(new Email(TrimmedEmail));
        }
        public static Result<Email> CreateVerified(string rawEmail)
        {
            EmailValidator validator = new EmailValidator();
            if (!validator.Validate(rawEmail, out List<string> errors))
                return Result<Email>.Failure(string.Join(",\n", errors));
            string TrimmedEmail = rawEmail.Trim();

            return Result<Email>.Success(new Email(TrimmedEmail,true));
        }

        public override string ToString() => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return IsVerified;
        }
    }
}
