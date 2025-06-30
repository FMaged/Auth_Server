namespace Application.Common.Exceptions.User
{
    public class PasswordException : AuthException
    {
        public PasswordException()
        {



        }


        public PasswordException(string message) : base(message)
        {


        }


        public PasswordException(string message, Exception innerException) : base(message, innerException)
        {



        }


    }
}
