using System.Diagnostics;
namespace T2Installer
{
    public static class ExceptionManager
    {
        static ExceptionManager()
        {
            if (!EventLog.SourceExists(ConstantVariable.LogSource))
            {
                EventLog.CreateEventSource(ConstantVariable.LogSource, ConstantVariable.LogSource);
            }
        }

        /// <summary>
        /// Manages the expection.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void LogException(string errorMessage)
        {
            EventLog.WriteEntry(ConstantVariable.LogSource, errorMessage, EventLogEntryType.Error);
        }
    }
}
