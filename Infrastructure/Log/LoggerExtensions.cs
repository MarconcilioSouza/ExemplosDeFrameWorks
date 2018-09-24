using System;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Log
{
    /// <summary>
    /// ILogger extension methods for common scenarios.
    /// </summary>
    public static class LoggerExtensions
    {
        #region Constants
        private const string MESSAGE_SUFFIX = ";{LogTimestamp};{ProcessID};{TaskID};";
        #endregion

        #region Helper Methods
        private static object[] AddLogContextData(this ILogger logger, object[] args) 
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            // If this logger is an instance of Log.Logger get the context data from it, else get a new one
            var traceContextData = (logger.GetType().Equals(typeof(Logger))) ? ((Logger)logger).TraceContext : new TraceContextData();

            var argsLength = args != null ? args.Length : 0;
            var newArgs = new object[argsLength + 3];

            newArgs[argsLength] = now;
            newArgs[argsLength + 1] = traceContextData.ProcessID;
            newArgs[argsLength + 2] = traceContextData.TaskID;

            if (argsLength > 0)
                args.CopyTo(newArgs, 0);

            return newArgs;
        }
        #endregion

        //------------------------------------------DEBUG------------------------------------------//
        #region LogDebug

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogDebug(logger, eventId, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogDebug(logger, eventId, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug(this ILogger logger, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogDebug(logger, 0, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogDebug(logger, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }
        #endregion

        //------------------------------------------INFORMATION------------------------------------------//
        #region LogInformation
        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Information(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogInformation(logger, eventId, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Information(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogInformation(logger, eventId, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Information(this ILogger logger, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogInformation(logger, 0, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Information(this ILogger logger, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogInformation(logger, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }
        #endregion

        //------------------------------------------WARNING------------------------------------------//
        #region LogWarning

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Warning(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogWarning(logger, eventId, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Warning(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogWarning(logger, eventId, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Warning(this ILogger logger, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogWarning(logger, 0, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Warning(this ILogger logger, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogWarning(logger, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }
        #endregion

        //------------------------------------------ERROR------------------------------------------//
        #region LogError
        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogError(logger, eventId, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogError(logger, eventId, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error(this ILogger logger, Exception exception, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogError(logger, 0, exception, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error(this ILogger logger, string message, params object[] args)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogError(logger, String.Concat(message, MESSAGE_SUFFIX), AddLogContextData(logger, args));
        }
        #endregion
    }
}
