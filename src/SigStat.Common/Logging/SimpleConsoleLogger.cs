using System;
using Microsoft.Extensions.Logging;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Logs messages to <see cref="Console"/>. 
    /// The font color is determined by the severity level.
    /// </summary>
    public class SimpleConsoleLogger : ILogger
    {
        /// <summary>
        /// The event is raised whenever a console message is logged
        /// </summary>
        /// <param name="consoleMessage"></param>
        public delegate void ConsoleMessageLoggedEventHandler(string consoleMessage);

        /// <summary>
        /// All events below this level will be filtered
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Occurs when a console message is logged
        /// </summary>
        public event ConsoleMessageLoggedEventHandler Logged;

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleConsoleLogger"/> with LogLevel set to <see cref="LogLevel.Information"/>.
        /// </summary>
        public SimpleConsoleLogger(): this(LogLevel.Information)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleConsoleLogger"/> with a custom <see cref="Microsoft.Extensions.Logging.LogLevel"/>.
        /// </summary>
        /// <param name="logLevel">Initial value for LogLevel.</param>
        public SimpleConsoleLogger(LogLevel logLevel)
        {
            LogLevel = logLevel;
        }
        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotSupportedException("Scopes are not supported by SimpleConsoleLogger");
        }

        /// <inheritdoc/>

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel;
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                formatter = (s, e) => s.ToString();

            var oldColor = Console.ForegroundColor;

            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case LogLevel.Information:
                case LogLevel.None:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
            string msg = formatter(state, exception);
            Console.WriteLine(msg);
            if (exception != null)
            {
                Console.WriteLine(exception);
            }
            Console.ForegroundColor = oldColor;

            Logged?.Invoke(msg);
        }
    }

}
