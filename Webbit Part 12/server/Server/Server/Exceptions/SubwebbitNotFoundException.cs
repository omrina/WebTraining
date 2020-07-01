using System;

namespace Server.Exceptions
{
    public class SubwebbitNotFoundException : Exception
    {
        public SubwebbitNotFoundException()
        {
        }

        public SubwebbitNotFoundException(string message) : base(message)
        {
        }

        public SubwebbitNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}