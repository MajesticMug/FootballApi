using System;

namespace Sports.Football.Core.ServiceClient.Model.Attributes
{
    public class ServiceClientResourceAttribute : Attribute
    {
        public string Uri { get; set; }
    }
}