using System;

namespace Server.Exceptions
{
    public class SubwebbitNameAlreadyTakenException : Exception
    {
        public SubwebbitNameAlreadyTakenException()
        {
        }

        public SubwebbitNameAlreadyTakenException(string message) : base(message)
        {
        }

        public SubwebbitNameAlreadyTakenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}