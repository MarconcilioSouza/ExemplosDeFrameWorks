using DryIoc;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteDryIoC.Generic
{
    public static class ProvadersValidator<T1> where T1 : class
    {
        public static IValidator GetValidators(Container container, Func<IEnumerable<IValidator<T1>>, IValidator<T1>> criterio = null)
        {
            var validador = container.Resolve<Lazy<IEnumerable<IValidator<T1>>>>();
            var validadores = validador.Value;
            // TODO: função para selecionar qual usar
            if (criterio != null)
                return criterio(validadores);

            return validadores.First();
        }
    }
}