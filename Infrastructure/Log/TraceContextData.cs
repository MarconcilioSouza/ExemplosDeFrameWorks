using System.Threading;

namespace Log
{
    public sealed class TraceContextData
    {
        #region Fields
        private string _processID;
        private string _taskID;
        #endregion

        #region ctor
        public TraceContextData()
        {
            // Initializes with Assembly friendly name and Thread ID
            _processID = Thread.GetDomain().FriendlyName; 
            _taskID = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public TraceContextData(string processId, string taskId)
        {
            _processID = processId;
            _taskID = taskId;
        }
        #endregion

        #region Properties
        public string ProcessID
        {
            set => _processID = value;
            get => _processID;
        }

        public string TaskID
        {
            set => _taskID = value;
            get => _taskID;
        }
        #endregion
    }
}
