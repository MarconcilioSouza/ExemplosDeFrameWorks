using ConectCar.Framework.Infrastructure.Log.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Log
{
    public static class LogHelper
    {
        #region Static Fields
        private static ILogger _internalLogger = null;
        private static ILoggerFactory _factory = null;
        private static IConfiguration _pipelineConfiguration = null;
        private static string _pipelineConfigFile = Resources.DefaultPipelineConfigFilename;
        private static DiagnosticPipeline _pipeline = null;
        private static AsyncLocal<TraceContextData> _traceContextCurrent = new AsyncLocal<TraceContextData>();
        private static IList<InternalErrorObject> _internalErrorBuffer = null;
        #endregion

        #region Private Static Properties
        private static ILoggerFactory LoggerFactory
        {
            get
            {
                // Isn't configured yet?
                if (_factory == null)
                {
                    // Create & configure the factory
                    _factory = new LoggerFactory();
                    ConfigureLogger();
                }
                return _factory;
            }
        }

        private static DiagnosticPipeline EventPipeline
        {
            get
            {
                // Isn't created yet?
                _pipeline = _pipeline ?? TryToCreatePipeline();

                return _pipeline;
            }
        }

        private static IConfiguration PipelineConfiguration
        {
            get
            {
                // Isn't readed yet?
                _pipelineConfiguration = _pipelineConfiguration ?? TryToReadConfigFile();

                return _pipelineConfiguration;
            }
        }
        #endregion

        #region Public Static Properties
        public static string PipelineConfigFile
        {
            set
            {
                // If not changed, get out of here
                if (value.Equals(_pipelineConfigFile))
                    return;

                _pipelineConfigFile = value;

                // If configuration file changed, reset configuration, pipeline & factory
                _pipelineConfiguration = null;

                _pipeline.Dispose();
                _pipeline = null;

                _factory.Dispose();
                _factory = null;

                // Then, try to configure it again with new Config File
                _internalLogger = CreateLogger(nameof(LogHelper));
            }

            get
            {
                return _pipelineConfigFile;
            }
        }

        public static TraceContextData TraceContextCurrent
        {
            set => _traceContextCurrent.Value = value;
            get => _traceContextCurrent.Value;
        }
        #endregion

        #region Private Static Methods
        private static DiagnosticPipeline TryToCreatePipeline()
        {
            DiagnosticPipeline pipeline = null;
            try
            {
                // Try to create the EventFlow Pipeline
                pipeline = DiagnosticPipelineFactory.CreatePipeline(PipelineConfiguration);
            }
            catch (Exception ex)
            {
                AddErrorToBuffer(ex, Resources.LogMsg_PipelineCreateError, nameof(EventPipeline), ex.Message);
            }
            return pipeline;
        }

        private static IConfiguration TryToReadConfigFile()
        {
            IConfiguration configuration = null;
            try
            {
                // Try to read and parse the EventFlow configuration data from PipelineConfigFile
                configuration = new ConfigurationBuilder()
                                    .AddJsonFile(PipelineConfigFile)
                                    .Build();
            }
            catch (Exception ex)
            {
                AddErrorToBuffer(ex, Resources.LogMsg_PipelineConfigReadError, nameof(TryToReadConfigFile), ex.Message);
            }
            return configuration;
        }

        private static void ConfigureLogger()
        {
            // Adds a default filter for mininum log level (Debug and above)
            LoggerFactory.WithFilter(new FilterLoggerSettings()
                                        {
                                            { "Default",  Microsoft.Extensions.Logging.LogLevel.Debug }
                                        });

            // Always add the Debug Provider with Trace log level, making easier to diagnostic issues in development time
            LoggerFactory.AddDebug(Microsoft.Extensions.Logging.LogLevel.Trace);

            if (EventPipeline == null)
            {
                // If isn't using Microsoft.Diagnostics.EventFlow provider, adds Console, at least
                // This is not supposed to happens in Production environments
                LoggerFactory.AddConsole(includeScopes: true);
            }
            else
            {
                LoggerFactory.AddEventFlow(EventPipeline);
            }

            // If there are exceptions catched on Pipeline configuration, send them to log and/or console
            EmitInternalErrorLogs();
        }
        #endregion

        #region Public Static Methods
        public static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);

        public static ILogger CreateLogger(Type categoryType) => LoggerFactory.CreateLogger(categoryType);

        public static ILogger CreateLogger<TCategory>() => LoggerFactory.CreateLogger<TCategory>();
        #endregion

        #region Exception Handling Methods & Classes
        private static void AddErrorToBuffer(Exception exception, string message, params object[] args)
        {
            _internalErrorBuffer = _internalErrorBuffer ?? new List<InternalErrorObject>();
            _internalErrorBuffer.Add(new InternalErrorObject(exception, message, args));
        }

        private static void EmitInternalErrorLogs()
        {
            if (_internalErrorBuffer == null || _internalErrorBuffer?.Count == 0)
                return;

            _internalLogger = _internalLogger ?? CreateLogger(nameof(LogHelper));

            foreach (var error in _internalErrorBuffer)
            {
#if DEBUG
                // In Debug Mode, print error to Debug Console
                Debug.WriteLine(error.Message, error.Args);
#endif
                _internalLogger.LogError(new EventId(99999), error.Exception, error.Message, error.Args);
            }
        }

        private class InternalErrorObject
        {
            public InternalErrorObject(Exception exception, string message, params object[] args)
            {
                Exception = exception;
                Message = message;
                Args = args;
            }
            public Exception Exception { get; set; }
            public string Message { get; set; }
            public object[] Args { get; set; }
        }
        #endregion
    }
}
