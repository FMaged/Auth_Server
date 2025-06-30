namespace Application.Common.Exceptions.User
{
    public class UserRegisterException : AuthException
    {
        public UserRegisterException()
        {



        }


        public UserRegisterException(string message) : base(message)
        {


        }


        public UserRegisterException(string message, Exception innerException) : base(message, innerException)
        {



        }
    }
}
