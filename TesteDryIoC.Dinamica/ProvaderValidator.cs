using DryIoc;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteDryIoC.Contratos;

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

    public static class ProvadersResolve<T1> where T1 : class
    {
        public static Lazy<T1> gercontainer()
        {
            Container container = new Container();
            container.Register<ISoma, Soma>();
            container.Register<ISubtracao, Subtracao>();

            var Resolve = container.Resolve<Lazy<T1>>();

            return Resolve;
        }
    }
}