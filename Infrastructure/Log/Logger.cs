using System;
using Microsoft.Extensions.Logging;

namespace Log
{
    public class Logger : ILogger
    {
        #region Fields
        private readonly ILogger _logHelper;
        #endregion

        #region Properties
        public TraceContextData TraceContext
        {
            set => LogHelper.TraceContextCurrent = value;
            // If there isn't current trace context data, instantiates a new one based on Assembly name and Thread Id
            get => LogHelper.TraceContextCurrent ?? new TraceContextData();
        }
        #endregion

        #region ctor
        protected Logger()
        {
        }

        public Logger(string categoryName) : this()
        {
            _logHelper = LogHelper.CreateLogger(categoryName);
        }

        public Logger(Type categoryType) : this()
        {
            _logHelper = LogHelper.CreateLogger(categoryType);
        }
        #endregion

        #region Methods
        public IDisposable BeginScope<TState>(TState state) => _logHelper.BeginScope<TState>(state);

        public bool IsEnabled(LogLevel logLevel) => _logHelper.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => _logHelper.Log<TState>(logLevel, eventId, state, exception, formatter);
        #endregion
    }

    // Only a wrapper for use typed Logger<TCategory> 
    public class Logger<TCategory> : Logger, ILogger<TCategory>
    {
        #region ctor
        public Logger() : base(typeof(TCategory))
        {
        }
        #endregion
    }

}
