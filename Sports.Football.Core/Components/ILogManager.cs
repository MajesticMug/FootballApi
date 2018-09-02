using System;

namespace Sports.Football.Core.Components
{
    public interface ILogManager
    {
        void Info(string message);
        void Warn(string message);
        void Warn(string message, Exception exception);
        void Error(string message, Exception exception);
    }
}