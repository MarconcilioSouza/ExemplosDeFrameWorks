using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDryIoC.Contratos;

namespace TesteDryIoC.Generic
{
    public class Subtracao : ISubtracao
    {
        public double Subtrair(double a, double b)
        {
            return a - b;
        }
    }
}
