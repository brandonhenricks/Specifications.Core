using System;

namespace Specifications.Core.Exceptions
{
    public class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException()
        {
        }

        public InvalidSpecificationException(string message) : base(message)
        {
        }

        public InvalidSpecificationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}