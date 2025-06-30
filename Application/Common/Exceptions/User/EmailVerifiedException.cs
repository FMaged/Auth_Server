namespace Application.Common.Exceptions.User
{
    public class EmailVerifiedException : AuthException
    {
        public EmailVerifiedException()
        {



        }


        public EmailVerifiedException(string message) : base(message)
        {


        }


        public EmailVerifiedException(string message, Exception innerException) : base(message, innerException)
        {



        }
    }
}
