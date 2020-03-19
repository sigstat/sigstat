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
        /// The event is raised whenever an error is logged
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="level">The level.</param>
        public delegate void ErrorEventHandler(string message, Exception exception, LogLevel level);

        /// <summary>
        /// All events below this level will be filtered
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Occurs when an error is logged
        /// </summary>
        public event ErrorEventHandler Logged;

        /// <summary>
        /// Initializes a SimpleConsoleLogger instance with LogLevel set to LogLevel.Information
        /// </summary>
        public SimpleConsoleLogger(): this(LogLevel.Information)
        {

        }

        /// <summary>
        /// Initializes an instance of SimpleConsoleLogger with a custom LogLevel
        /// </summary>
        /// <param name="logLevel">initial value for LogLevel</param>
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
            Logged?.Invoke(msg, exception, logLevel);

            Console.ForegroundColor = oldColor;
        }
    }

}
