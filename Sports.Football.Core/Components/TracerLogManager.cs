using System;
using System.Diagnostics;

namespace Sports.Football.Core.Components
{
    public class TracerLogManager : ILogManager
    {
        public void Error(string message, Exception exception)
        {
            Trace.TraceError($"Error:'{message}' - Exception:'{exception.Message}' - StackTrace: {exception.StackTrace}");
        }

        public void Info(string message)
        {
            Trace.TraceInformation($"Info:'{message}'");
        }

        public void Warn(string message)
        {
            Trace.TraceWarning($"Warning:'{message}'");
        }

        public void Warn(string message, Exception exception)
        {
            Trace.TraceError($"Warning:'{message}' - Exception:'{exception.Message}' - StackTrace: {exception.StackTrace}");
        }
    }
}