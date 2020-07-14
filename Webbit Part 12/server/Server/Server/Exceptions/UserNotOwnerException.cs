using System;

namespace Server.Exceptions
{
    public class UserNotOwnerException : Exception
    {
        public UserNotOwnerException()
        {
        }

        public UserNotOwnerException(string message) : base(message)
        {
        }

        public UserNotOwnerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}