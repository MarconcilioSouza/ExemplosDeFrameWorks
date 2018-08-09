using System.Data.SqlClient;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Caches.SysCache2;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;

namespace NHSecondLevelCache.Code
{
    public class SessionHelper
    {
        private static string connetionString = ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString;

        private static SqlConnectionStringBuilder _conn = new SqlConnectionStringBuilder(connetionString);
        private static ISessionFactory _sessionFactory;

        public static ISession CreateSession()
        {
            ISessionFactory factory = GetSessionFactory();
            return factory.OpenSession();
        }

        public static ISessionFactory GetSessionFactory()
        {
            if (null == _sessionFactory)
            {
                FluentConfiguration nhConfig = Fluently.Configure()
                           .Database(
                               MsSqlConfiguration.MsSql2008.ConnectionString(
                                   c =>
                                   c.Database(_conn.InitialCatalog).Server(_conn.DataSource).TrustedConnection()))
                           .Mappings(m =>
                                     m.FluentMappings.AddFromAssemblyOf<Itens>()
                                         .Conventions.Add(DefaultLazy.Never()));

                nhConfig.Cache(c => c.ProviderClass<SysCacheProvider>().UseSecondLevelCache());
                _sessionFactory = nhConfig.ExposeConfiguration(v => new SchemaExport(v).Create(false, false)).BuildSessionFactory();

            }
            return _sessionFactory;
        }
    }
}
