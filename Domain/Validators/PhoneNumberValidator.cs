



using Domain.Interfaces.Services;

namespace Domain.Validators
{
    public class PhoneNumberValidator : IValidator<string>
    {

        public PhoneNumberValidator()
        {

        }
        public bool Validate(string value, out List<string> errors)
        {
            //  z.B. 0049/17620137818
            errors = new List<string>();
            string[] parts = value.Split("/");
            return Validate(parts[0], parts[1], out errors);
        }
        public bool Validate(string countryCode, string number, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrWhiteSpace(countryCode)||string.IsNullOrWhiteSpace(number))
                errors.Add("Phone number cannot be empty.");
            if (!IsValidPhoneNumber(countryCode, number))
                errors.Add("Invalid phone number format.");


                



            return true;
        }

        private static bool IsValidPhoneNumber( string countryCode, string number)
        {
            // Basic validation - adjust based on your requirements
            // You might want to use a more sophisticated validation library
            var digitsOnly = new string(number.Where(char.IsDigit).ToArray());

            return countryCode switch
            {
                "+1" => digitsOnly.Length == 10, // US/Canada
                "+44" => digitsOnly.Length == 10, // UK
                                                  // Add more country codes as needed
                _ => digitsOnly.Length >= 8 && digitsOnly.Length <= 15 // General international
            };
        }


    }
}
