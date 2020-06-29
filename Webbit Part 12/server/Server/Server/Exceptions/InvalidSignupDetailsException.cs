using System;

namespace Server.Exceptions
{
    public class InvalidSignupDetailsException : Exception
    {
        public InvalidSignupDetailsException()
        {
        }

        public InvalidSignupDetailsException(string message) : base(message)
        {
        }

        public InvalidSignupDetailsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}