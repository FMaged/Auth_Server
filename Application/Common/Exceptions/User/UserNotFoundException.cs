namespace Application.Common.Exceptions.User
{
    public class UserNotFoundException : AuthException
    {

        public UserNotFoundException()
        {



        }


        public UserNotFoundException(string message) : base(message)
        {


        }


        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {



        }

    }
}
