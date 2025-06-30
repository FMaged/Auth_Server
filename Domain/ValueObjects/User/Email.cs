

using Domain.Shared;
using Domain.Validators;

namespace Domain.ValueObjects.User
{
    public sealed class Email:ValueObject
    {
        public string Value { get;}
        public bool IsVerified { get; }

        private Email(string value,bool isVerified) 
        {
            Value = value;
            IsVerified = isVerified;
        }
        public static Result<Email> Create(string rawEmail, bool isVerified = false)
        {
            EmailValidator validator=new EmailValidator();
            if(!validator.Validate(rawEmail,out List<string> errors))
                return Result<Email>.Failure(string.Join(",\n", errors));
            string TrimmedEmail = rawEmail.Trim();

            return Result<Email>.Success(new Email(TrimmedEmail,isVerified));
        }
        public Result<Email> Verify()
        {
            return Create(Value,true);
        }


        public bool isEmailVerified()=> IsVerified;
        public override string ToString() => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return IsVerified;
        }
    }
}
