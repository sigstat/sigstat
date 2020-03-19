using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Forwards messages to <see cref="ILogger"/> components.
    /// </summary>
    public class CompositeLogger : ILogger
    {
        /// <summary>
        /// The list of <see cref="ILogger"/> components that messages are forwarded to. Empty by default.
        /// </summary>
        public List<ILogger> Loggers { get; set; } = new List<ILogger>();

        /// <summary>
        /// Calls <see cref="ILogger.BeginScope{TState}(TState)"/> on each component.
        /// </summary>
        public IDisposable BeginScope<TState>(TState state)
        {
            Loggers.ForEach(l => l.BeginScope(state));

            //TODO: return an object to dispose scope on each component
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns true if any of the <see cref="ILogger"/> components are enabled on the specified <see cref="LogLevel"/>.
        /// </summary>
        public bool IsEnabled(LogLevel logLevel)
        {
            return Loggers.Any(l => l.IsEnabled(logLevel));
        }

        /// <summary>
        /// Forwards the message to each <see cref="ILogger"/> component.
        /// </summary>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Loggers.ForEach(l =>
                l.Log(logLevel, eventId, state, exception, formatter));
        }
    }
}
