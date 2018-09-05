using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDryIoC.Contratos;

namespace TesteDryIoC.Generic
{
    public class Soma : ISoma
    {
        public double Somar(double a, double b)
        {
            return a + b;
        }
    }
}
