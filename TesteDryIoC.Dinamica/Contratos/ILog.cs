using System;

namespace TesteDryIoC
{
    public interface ILog
    {
        void Log(Exception ex);
        void Log(string msg);
    }
}
