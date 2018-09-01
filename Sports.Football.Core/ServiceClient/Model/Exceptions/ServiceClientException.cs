using System;

namespace Sports.Football.Core.ServiceClient.Model.Exceptions
{
    public class ServiceClientException : Exception
    {
        public ServiceClientException(string message, Exception inner = null) : base(message, inner)
        {

        }
    }
}