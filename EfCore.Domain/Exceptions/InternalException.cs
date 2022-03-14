using System;

namespace EfCore.Domain.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException(string message, Exception innerException): base(message, innerException)
        {

        }

        public InternalException(string message): base(message)
        {

        }

        public InternalException()
        {

        }
    }
}
