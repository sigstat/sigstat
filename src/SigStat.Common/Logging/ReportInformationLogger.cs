using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Logger for logging report informations
    /// </summary>
    public class ReportInformationLogger : ILogger
    {

        /// <summary>
        /// The event is raised whenever an error is logged
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="level">The level.</param>
        public delegate void ErrorEventHandler(string message, Exception exception, LogLevel level);

        /// <summary>
        /// Stored logs that contain information for the report
        /// </summary>
        private List<SigStatLogState> reportLogs;

        /// <summary>
        /// Public read-only interface to reach logged states.
        /// </summary>
        public IReadOnlyList<SigStatLogState> ReportLogs { get { return reportLogs; } }

        /// <summary>
        /// Occurs when an error is logged
        /// </summary>
        public event ErrorEventHandler Logged;

        /// <summary>
        /// Initializes an instance of ReportInformationLogger
        /// </summary>
        public ReportInformationLogger()
        {
            reportLogs = new List<SigStatLogState>();
        }
        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotSupportedException("Scopes are not supported by SimpleConsoleLogger");
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            //TODO infó log kezelése
            if (state is SigStatLogState)
            {
                this.reportLogs.Add(state as SigStatLogState);
            }

            if (exception != null)
            {
                Console.WriteLine(exception);
            }
            Logged?.Invoke("", exception, logLevel);
        }
        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

    }
}
