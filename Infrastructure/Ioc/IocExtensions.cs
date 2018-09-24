using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ioc
{
    /// <summary>
    /// Extensions para apoiar o uso do container de uma maneira mais otimizada.
    /// </summary>
    /// <remarks>
    /// Conjunto de extension methods para apoiar o uso do container.
    /// </remarks>
    public static class IocExtensions
    {        
        #region [Methods]

        /// <summary>
        /// Registra um serviço relacionado a sua implementação apenas uma vez no container.
        /// </summary>       
        /// <param name="container">Container do DryIoc.</param>
        /// <param name="serviceType">Type do serviço.</param>
        /// <param name="implementationType">Type da implementação.</param>
        public static void RegisterOnce(this Container container, Type serviceType, Type implementationType)
        {
            var registrations = container.GetServiceRegistrations();
            if(!registrations.ToList()
                .Any(x=> x.Factory.ImplementationType== implementationType && x.ServiceType == serviceType))
            {
                container.Register(serviceType, implementationType);
            }
                        
        }

        /// <summary>
        /// Resolve as implementações a partir de um determinado serviço.
        /// </summary>
        /// <remarks>
        /// Responsável por carregar as implementações usando lazy loading. Caso haja mais de uma implentação
        /// será retornado a primeira encontrada ou será utilizado um filtro sobre as implementações para determinar
        /// qual deverá ser escolhida.
        /// </remarks>
        /// <typeparam name="TService">Tipo do serviço que será resolvido.</typeparam>
        /// <param name="container">Container do DryIoc.</param>
        /// <param name="filter">Filtro opcional para ser aplicado quando existe mais de uma implementação.</param>
        /// <returns>Implementação de um tipo de de serviço.</returns>
        public static TService ResolveImplementations<TService>(this Container container, Func<IEnumerable<TService>, TService> filter = null)
        {
            var lazyImplementations = container.Resolve<Lazy<IEnumerable<TService>>>();
            var implementations = lazyImplementations.Value;
            if (filter != null)
                return filter(implementations);

            return implementations.FirstOrDefault();
        }

        #endregion

    }
}
