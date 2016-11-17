using System;
using System.Linq;
using FaPA.AppServices;
using FaPA.Data;
using FaPA.Infrastructure.Helpers;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace FaPaTets.DbSetUp
{
    [TestFixture]
    public class SchemaTest
    {
        [Test]
        public void CanGenerateSchemaSqlServerDb()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration( c =>
            {
                c.Dialect<MsSql2008Dialect>();
                c.ConnectionString = StoreAccess.ConnString;
                c.SchemaAction = SchemaAutoAction.Create;
            } );

            /* Add the mapping we defined: */
            var mapper = new ModelMapper();
            mapper.AddMappings( typeof( FatturaMap ).Assembly.GetTypes().Where( t => t?.Namespace != null &&
               t.Namespace.StartsWith( "FaPA.Data" ) ) );

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping( mapping );

            var schemaUpdate = new SchemaUpdate( cfg );
            schemaUpdate.Execute( Console.WriteLine, true );
        }

        [Test]
        public void CanGenerateSchemaInMemoryDb()
        {
            var schemaUpdate = new SchemaUpdate( NHibernateHelper.Configuration );
            schemaUpdate.Execute( Console.WriteLine, true );
        }
    }
}
