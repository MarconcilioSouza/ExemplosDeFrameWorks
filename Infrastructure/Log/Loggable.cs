using Microsoft.Extensions.Logging;

namespace Log
{
    /// <summary>
    /// Classe base para utilização em cenários que demandem log.
    /// </summary>
    /// <remarks>
    /// Toda classe que necessitar realizar log internamente poderá derivar desta classe.
    /// </remarks>
    public abstract class Loggable
    {
        #region Fields
        /// <summary>
        /// Representa a API para executar logs no sistema.
        /// </summary>
        private readonly ILogger _log;
        #endregion

        #region Properties
        /// <summary>
        /// Expõe a API de logs para a classe derivada.
        /// </summary>
        public ILogger Log => _log;

        /// <summary>
        /// Expõe, para a classe derivada, os dados de contexto <see cref="TraceContextData"/> para tracing dos logs.
        /// </summary>
        public TraceContextData TraceContext
        {
            set => ((Log.Logger)_log).TraceContext = value;
            get => ((Log.Logger)_log).TraceContext;
        }
        #endregion

        #region ctor
        /// <summary>
        /// Inicializa a classe base para registrar logs.
        /// </summary>
        protected Loggable()
        {
            _log = new Logger(GetType());
        }
        #endregion

    }
}
