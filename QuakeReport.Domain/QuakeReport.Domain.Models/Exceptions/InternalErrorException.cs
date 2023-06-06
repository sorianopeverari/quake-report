using System;

namespace QuakeReport.Domain.Models.Exceptions
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException()
        {
        }

        public InternalErrorException(string message)
        : base(message)
        {
        }

        public InternalErrorException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}