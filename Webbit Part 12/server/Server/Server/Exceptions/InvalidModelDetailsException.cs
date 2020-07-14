using System;

namespace Server.Exceptions
{
    public class InvalidModelDetailsException : Exception
    {
        public InvalidModelDetailsException()
        {
        }

        public InvalidModelDetailsException(string message) : base(message)
        {
        }

        public InvalidModelDetailsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}