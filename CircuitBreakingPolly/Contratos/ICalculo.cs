using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakingPolly.Contratos
{
    public interface ICalculo
    {
        double Calcular(double a, double b);
    }
}
