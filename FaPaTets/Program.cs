using System.Linq;
using FaPaTets.DbSetUp;
using FaPA.Core;
using FaPA.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using Environment = System.Environment;

namespace FaPaTets
{
    
    class Program
    {
        public static void Main()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11001.xml";
            var ouPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) + @"\file.xml";

            const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
            Configuration cfg = new Configuration()
            .DataBaseIntegration( db =>
            {
                db.ConnectionString = CONNECTION_STRING;
                db.Dialect<SQLiteDialect>();
            } );

            /* Add the mapping we defined: */
            var mapper = new ModelMapper();
            mapper.AddMappings( typeof( FatturaMap ).Assembly.GetTypes().Where( t => t?.Namespace != null && 
            t.Namespace.StartsWith( "FaPA.Data" ) ) );

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping( mapping );
            //cfg.AddAuxiliaryDatabaseObject(CreateHighLowScript(modelInspector, Assembly.GetExecutingAssembly().GetExportedTypes()));

            var sessionfactory = cfg.BuildSessionFactory();
            var databaseScope = new SQLiteDatabaseScope( cfg, sessionfactory );
            
            //the key point is pass your session.Connection here
            //new SchemaExport( cfg ).Execute( true, true, false, session1.Connection, null );
          
            using (ISession session1 = databaseScope.OpenSession() )
            using ( ITransaction tx = session1.BeginTransaction() )
            {
                var anagraficaCedente = new Fornitore() {CodiceFiscale = "00"};
                var anagraficaCommittente = new Committente() { CodiceFiscale = "00" };
                var fattura = new Fattura()
                {
                    //DataFatturaDB = DateTime.Now,
                    //AnagraficaCedenteDB = anagraficaCedente,
                    //AnagraficaCommittenteDB = anagraficaCommittente
                };

                anagraficaCedente.Fatture.Add(fattura);
                anagraficaCommittente.Fatture.Add(fattura);

                session1.Save( anagraficaCedente );
                session1.Save( anagraficaCommittente );
                session1.Save( fattura );

                tx.Commit();
            }

            using (ISession session2 = databaseScope.OpenSession())
            using (ITransaction tx = session2.BeginTransaction())
            {
                var l = session2.QueryOver<Anagrafica>().Fetch(f => f.Fatture).Eager.List<Anagrafica>();
                var f1 = l.First().Fatture;
                tx.Commit();
            }

        }


    }


}
