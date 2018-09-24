using NLog.Fluent;
using System;

namespace Log4Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Log.Debug("TESTE  CCCCC");
            Console.ReadKey();
        }
    }
}
