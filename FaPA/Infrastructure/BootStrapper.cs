using System.Linq;
using FaPA.AppServices;
using FaPA.Data;
using FaPA.DomainServices;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Validator.Engine;

namespace FaPA.Infrastructure
{
    public static class BootStrapper
    {
        public static ISessionFactory SessionFactory
        {
            get;
            private set;
        }

        //private static Configuration Configuration { get; set; }

        public static void Initialize()
        {
            //log4net.Config.XmlConfigurator.Configure();

            var cfg = new Configuration();
            //cfg.Proxy(p => p.ProxyFactoryFactory<PropertyChangedProxyFactoryFactory>());
            cfg.DataBaseIntegration(c =>
            {
                c.BatchSize = 50;
                c.Dialect<MsSql2008Dialect>();
                c.ConnectionString = StoreAccess.ConnString;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
                c.AutoCommentSql = true;
            });

            var validatorEngine = new ValidatorEngine();
            
            cfg.ConfigureNHibernateValidator( validatorEngine );

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(FatturaMap).Assembly.GetTypes().Where(t => t?.Namespace != null &&
            t.Namespace.StartsWith("FaPA.Data")));

            cfg.AddMapping( mapper.CompileMappingForAllExplicitlyAddedEntities() );

            var context = typeof( ThreadStaticSessionContext ).AssemblyQualifiedName;
            cfg.SetProperty( Environment.CurrentSessionContextClass, context );
            
            SessionFactory = cfg.BuildSessionFactory();

            new BasicSharedEngineProvider( validatorEngine ).UseMe();

            NHibernateStaticContainer.SessionFactory = SessionFactory;
        }
    }
}




