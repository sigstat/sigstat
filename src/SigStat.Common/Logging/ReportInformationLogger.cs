using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Logger for logging report informations.
    /// </summary>
    public class ReportInformationLogger : ILogger
    {

        /// <summary>
        /// The event is raised whenever a SigStatLogState is logged.
        /// </summary>
        public delegate void LogStateLoggedEventHandler(SigStatLogState logState);

        /// <summary>
        /// Stored logs that contain information for the report.
        /// </summary>
        private List<SigStatLogState> reportLogs;

        /// <summary>
        /// Public read-only interface to reach logged states.
        /// </summary>
        public IReadOnlyList<SigStatLogState> ReportLogs { get { return reportLogs; } }

        /// <summary>
        /// Occurs when an error is logged.
        /// </summary>
        public event LogStateLoggedEventHandler Logged;

        /// <summary>
        /// Initializes a new instance of <see cref="ReportInformationLogger"/>.
        /// </summary>
        public ReportInformationLogger()
        {
            reportLogs = new List<SigStatLogState>();
        }

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotSupportedException("Scopes are not supported by ReportInformationLogger");
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (state is SigStatLogState)
            {
                this.reportLogs.Add(state as SigStatLogState);
                Logged?.Invoke(state as SigStatLogState);
            }
        }

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

    }
}
