using DryIoc;

namespace Ioc
{
    /// <summary>
    /// Representa um container de IOC responsável pela resolução de serviços e implementações.
    /// </summary>
    public static class IocContainer
    {
        #region [Properties]

        /// <summary>
        /// Representa um container DryIoc.
        /// </summary>
        private static Container _container;

        #endregion

        #region [Methods]

        /// <summary>
        /// Retorna um container do DryIoc.
        /// </summary>
        /// <returns>Container</returns>
        public static Container Container
        {
            get {
                if (_container == null)
                    _container = new Container();
                return _container;
            }
        }        
        
        #endregion


    }
}
