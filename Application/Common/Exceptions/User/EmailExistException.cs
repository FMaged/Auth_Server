namespace Application.Common.Exceptions.User
{
    public class EmailExistException : AuthException
    {
        public EmailExistException()
        {



        }


        public EmailExistException(string message) : base(message)
        {


        }


        public EmailExistException(string message, Exception innerException) : base(message, innerException)
        {



        }



    }
}
