using System;

namespace Sports.Football.Core.Model.Exceptions
{
    public class CompetitionAlreadyImportedException : Exception
    {
        public CompetitionAlreadyImportedException()
        {
        }

        public CompetitionAlreadyImportedException(string message) : base(message)
        {
        }
    }
}