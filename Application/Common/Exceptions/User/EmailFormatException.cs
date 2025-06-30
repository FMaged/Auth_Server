namespace Application.Common.Exceptions.User
{
    public class EmailFormatException : AuthException
    {
        public EmailFormatException()
        {



        }


        public EmailFormatException(string message) : base(message)
        {


        }


        public EmailFormatException(string message, Exception innerException) : base(message, innerException)
        {



        }
    }
}
