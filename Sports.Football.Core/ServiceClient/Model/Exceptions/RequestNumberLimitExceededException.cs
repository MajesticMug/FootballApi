using System;

namespace Sports.Football.Core.ServiceClient.Model.Exceptions
{
    public class RequestNumberLimitExceededException : Exception
    {
        public RequestNumberLimitExceededException()
        {
        }

        public RequestNumberLimitExceededException(string message) : base(message)
        {
        }

        public RequestNumberLimitExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}