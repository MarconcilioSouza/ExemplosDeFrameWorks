using System;

namespace CircuitBreakingPolly
{
    public interface ILog
    {
        void Log(Exception ex);
        void Log(string msg);
    }
}
