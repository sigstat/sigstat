using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SigStat.Common
{
    /// <summary>
    /// ILoggerObject extension methods for common scenarios.
    /// </summary>
    /// <remarks>
    /// Note to framework developers: you may extend this class with additional overloads if they are required
    /// </remarks>
    public static class ILoggerObjectExtensions
    {
        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogError(this ILoggerObject obj, string message, params object[] args)
        {
            obj.Logger?.LogError(message, args);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogError(this ILoggerObject obj, Exception exception, string message, params object[] args)
        {
            obj.Logger?.LogError(exception, message, args);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogInformation(this ILoggerObject obj, string message, params object[] args)
        {
            obj.Logger?.LogInformation(message, args);
        }


        /// <summary>
        /// Formats and writes an warning log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning(this ILoggerObject obj, string message, params object[] args)
        {
            obj.Logger?.LogWarning(message, args);
        }

        /// <summary>
        /// Formats and writes an warning log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning(this ILoggerObject obj, Exception exception, string message, params object[] args)
        {
            obj.Logger?.LogWarning(exception, message, args);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogTrace(this ILoggerObject obj, string message, params object[] args)
        {
            obj.Logger?.LogTrace(message, args);
        }

        /// <summary>
        /// Formats and writes an critical error log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogCritical(this ILoggerObject obj, string message, params object[] args)
        {
            obj.Logger?.LogCritical(message, args);
        }

        /// <summary>
        /// Formats and writes an debug log message.
        /// </summary>
        /// <param name="obj">The SigStat.Common.ILoggerObject containing the Logger to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: "User {User} logged in from {Address}"</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogDebug(this ILoggerObject obj, string message, params object[] args)
        {
            obj.Logger?.LogDebug(message, args);
        }

    }
}
