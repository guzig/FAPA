using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Validator.Engine;
using System.Linq;
using WAFEL.Data;
using WAFEL.Core.CORE.DomainServices;
using WAFEL.Core.CORE.DomainServices.Helpers;

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

            var mapTypes = typeof(FatturaMap).Assembly.GetTypes()
	            .Where(t => t?.Namespace != null && t.Namespace.StartsWith("WAFEL.Data") && t.Name.EndsWith( "Map" ))
	            .ToList(  );

            mapper.AddMappings(mapTypes);

            cfg.AddMapping( mapper.CompileMappingForAllExplicitlyAddedEntities() );

            var context = typeof( ThreadStaticSessionContext ).AssemblyQualifiedName;
            cfg.SetProperty(NHibernate.Cfg.Environment.CurrentSessionContextClass, context );
            
            SessionFactory = cfg.BuildSessionFactory();

            new BasicSharedEngineProvider( validatorEngine ).UseMe();

            NHibernateStaticContainer.SessionFactory = SessionFactory;
        }
    }
}




