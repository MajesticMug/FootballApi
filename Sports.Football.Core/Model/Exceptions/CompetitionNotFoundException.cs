using System;

namespace Sports.Football.Core.Model.Exceptions
{
    public class CompetitionNotFoundException : Exception
    {
        public CompetitionNotFoundException()
        {
        }

        public CompetitionNotFoundException(string message) : base(message)
        {
        }
    }
}