using System;

namespace Server.Exceptions
{
    public class UnauthenticatedRequestException : Exception
    {
        public UnauthenticatedRequestException()
        {
        }

        public UnauthenticatedRequestException(string message) : base(message)
        {
        }

        public UnauthenticatedRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}