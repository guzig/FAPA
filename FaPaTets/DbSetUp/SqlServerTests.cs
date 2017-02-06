using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using FaPA.AppServices;
using FaPA.Core;
using FaPA.Data;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;
using NHibernate.Validator.Engine;

namespace FaPaTets.DbSetUp
{
    [TestFixture]
    class SqlServerTests
    {
        public static IDictionary<string, string> _provincie = new Dictionary<string, string>()
        {
            #region provincie
            {"AG", "Agrigento"},
            {"AL", "Alessandria"},
            {"AN", "Ancona"},
            {"AO", "Aosta"},
            {"AR", "Arezzo"},
            {"AP", "Ascoli Piceno"},
            {"AT", "Asti"},
            {"AV", "Avellino"},
            {"BA", "Bari"},
            {"BT", "Barletta-Andria-Trani"},
            {"BL", "Belluno"},
            {"BN", "Benevento"},
            {"BG", "Bergamo"},
            {"BI", "Biella"},
            {"BO", "Bologna"},
            {"BZ", "Bolzano"},
            {"BS", "Brescia"},
            {"BR", "Brindisi"},
            {"CA", "Cagliari"},
            {"CL", "Caltanissetta"},
            {"CB", "Campobasso"},
            {"CE", "Caserta"},
            {"CT", "Catania"},
            {"CZ", "Catanzaro"},
            {"CH", "Chieti"},
            {"CO", "Como"},
            {"CS", "Cosenza"},
            {"CR", "Cremona"},
            {"KR", "Crotone"},
            {"CN", "Cuneo"},
            {"EN", "Enna"},
            {"FE", "Ferrara"},
            {"FM", "Fermo"},
            {"FI", "Firenze"},
            {"FG", "Foggia"},
            {"FO", "Forli'-Cesena"},
            {"FC", "Forli'-Cesena"},
            {"FR", "Frosinone"},
            {"GE", "Genova"},
            {"GO", "Gorizia"},
            {"GR", "Grosseto"},
            {"IM", "Imperia"},
            {"IS", "Isernia"},
            {"SP", "La Spezia"},
            {"AQ", "L'Aquila"},
            {"LT", "Latina"},
            {"LE", "Lecce"},
            {"LC", "Lecco"},
            {"LI", "Livorno"},
            {"LO", "Lodi"},
            {"LU", "Lucca"},
            {"MC", "Macerata"},
            {"MB", "Monza e Brianza"},
            {"MN", "Mantova"},
            {"MS", "Massa-Carrara"},
            {"MT", "Matera"},
            {"ME", "Messina"},
            {"MI", "Milano"},
            {"MO", "Modena"},
            {"NA", "Napoli"},
            {"NO", "Novara"},
            {"NU", "Nuoro"},
            {"OR", "Oristano"},
            {"PD", "Padova"},
            {"PA", "Palermo"},
            {"PR", "Parma"},
            {"PV", "Pavia"},
            {"PG", "Perugia"},
            {"PS", "Pesaro e Urbino"},
            {"PU", "Pesaro e Urbino"},
            {"PE", "Pescara"},
            {"PC", "Piacenza"},
            {"PI", "Pisa"},
            {"PT", "Pistoia"},
            {"OG", "Provincia dell'Ogliastra"},
            {"PN", "Pordenone"},
            {"PZ", "Potenza"},
            {"PO", "Prato"},
            {"RG", "Ragusa"},
            {"RA", "Ravenna"},
            {"RC", "Reggio di Calabria"},
            {"RE", "Reggio nell'Emilia"},
            {"RI", "Rieti"},
            {"RN", "Rimini"},
            {"RM", "Roma"},
            {"RO", "Rovigo"},
            {"SA", "Salerno"},
            {"SS", "Sassari"},
            {"OT", "Sassari"},
            {"SV", "Savona"},
            {"SI", "Siena"},
            {"SR", "Siracusa"},
            {"SO", "Sondrio"},
            {"TA", "Taranto"},
            {"TE", "Teramo"},
            {"TR", "Terni"},
            {"TO", "Torino"},
            {"TP", "Trapani"},
            {"TN", "Trento"},
            {"TV", "Treviso"},
            {"TS", "Trieste"},
            {"UD", "Udine"},
            {"VA", "Varese"},
            {"VE", "Venezia"},
            {"VB", "Verbano-Cusio-Ossola"},
            {"VC", "Vercelli"},
            {"VR", "Verona"},
            {"VV", "Vibo Valentia"},
            {"VI", "Vicenza"},
            {"VT", "Viterbo"},
            {"VS", "Provincia del Medio Campidano"},
            {"CI", "Provincia di Carbonia-Iglesias"}
           
            #endregion
        };

        [Test]
        public void CanCreateSqlServerDb()
        {
            DropDatabase();
            CreateDatabase();

            var cfg = new Configuration();
            cfg.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2008Dialect>();
                c.ConnectionString = StoreAccess.ConnString;
                c.SchemaAction = SchemaAutoAction.Create;
            });

            /* Add the mapping we defined: */
            var mapper = new ModelMapper();
            mapper.AddMappings( typeof(FatturaMap).Assembly.GetTypes().Where(t => t?.Namespace != null &&
            t.Namespace.StartsWith( "FaPA.Data" ) ) );

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            //cfg.AddAuxiliaryDatabaseObject(CreateHighLowScript(modelInspector, Assembly.GetExecutingAssembly().GetExportedTypes()));

            var validatorEngine = new ValidatorEngine();
            new BasicSharedEngineProvider(validatorEngine).UseMe();
            cfg.ConfigureNHibernateValidator(validatorEngine);
            //cfg.CreateIndexesForForeignKeys();
            var sessionfactory = cfg.BuildSessionFactory();

            FaPA.DomainServices.NHibernateStaticContainer.SessionFactory = sessionfactory;

            //new SchemaExport( cfg ).Execute( true, true, false  );

            using (ISession session1 = sessionfactory.OpenSession())
            using (ITransaction tx = session1.BeginTransaction())
            {
                var committente = new Anagrafica()
                {
                    CodUfficioPa = "EZ8816",
                    Denominazione = "Comune di Crucoli",
                    CodiceFiscale = "00297210791",
                    PIva = "00297210791",
                    Comune = "Crucoli",
                    Cap = "88812",
                    Civico = "01",
                    Provincia = "KR",
                    Nazione = "IT",
                    Indirizzo = "Via Roma 1"
                };

                var anagraficaCedente = new Anagrafica()
                {
                    Denominazione = "Comune di Isola di Capo Rizzuto",
                    CodiceFiscale = "81004130795",
                    PIva = "01939480792",
                    Comune = "Isola di Capo Rizzuto",
                    Cap = "88841",
                    Civico = "01",
                    Provincia = "KR",
                    Nazione = "IT",
                    Indirizzo = "Piazza Falcone e Borsellino"
                };

                var anagraficaCommittente = new Anagrafica()
                {
                    Denominazione = "PA·WARE",
                    CodiceFiscale = "GZZMRA65R50D181Y",
                    PIva = "03462320791",
                    Comune = "Cropani",
                    Cap = "88051",
                    Civico = "snc",
                    Provincia = "CZ",
                    Nazione = "IT",
                    Indirizzo = "Via Lombardia"
                };
                
                session1.Save(anagraficaCedente);
                session1.Save(anagraficaCommittente);
                session1.Save(committente);

                tx.Commit();
            }

            LoadComuni( sessionfactory.OpenStatelessSession() );
        }

        private static void DropDatabase()
        {
            var tmpConn = new SqlConnection();

            tmpConn.ConnectionString = "SERVER = " + StoreAccess.SourcePath + "; DATABASE = master; " +
            "User ID =" + StoreAccess.User + " ; Pwd =" + StoreAccess.Password;

            const string sqlDropDbQuery = " IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'FEPA') DROP DATABASE FEPA ";

            var myCommand = new SqlCommand(sqlDropDbQuery, tmpConn);
            try
            {
                tmpConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                tmpConn.Close();
            }
        }

        private static void CreateDatabase()
        {
            var tmpConn = new SqlConnection();

            tmpConn.ConnectionString = "SERVER = " + StoreAccess.SourcePath + "; DATABASE = master; " +
            "User ID =" + StoreAccess.User + " ; Pwd =" + StoreAccess.Password;

            var sqlCreateDBQuery = "CREATE DATABASE FEPA ON PRIMARY "
                                      + @" (NAME = EMUL_Data, FILENAME = '" + StoreAccess.DbFullPath + "', SIZE = 10MB, FILEGROWTH = 10% ) "
                                      + @" LOG ON (NAME = EMUL_Log,  FILENAME = '" + StoreAccess.DbLogFullPath + "', SIZE = 10MB, FILEGROWTH = 10% ) ";

            var sqlCreateDbQuery = sqlCreateDBQuery;

            var myCommand = new SqlCommand(sqlCreateDbQuery, tmpConn);
            try
            {
                tmpConn.Open();
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                tmpConn.Close();
            }
        }

        private static void LoadComuni(IStatelessSession session)
        {
            //BootStrapper.Initialize();
            //var session = BootStrapper.SessionFactory.OpenStatelessSession();
            var nomeFileCap =  @"C:\Users\tonio\Desktop\listacomuniConCAP.txt";
                              //@"C:\Users\Devs\Desktop\listacomuniConCAP.txt";

            var dictCap = new Dictionary<string,string>(); 

            using (TextReader readerCap = new StreamReader(nomeFileCap))
            {
                string line;
                while ( (line = readerCap.ReadLine() ) != null )
                {
                    var fields = line.Split(';').ToArray();
                    var nomeComune = fields[1];
                    var cap = fields[5];
                    if ( dictCap.ContainsKey(nomeComune) ) continue;
                    dictCap.Add( nomeComune, cap );
                }
            }

            using (var tx = session.BeginTransaction())
            {
                var nomeFile = @"C:\Users\tonio\Desktop\elenco-comuni-italiani.csv";
                               //@"C:\Users\Devs\Desktop\elenco-comuni-italiani.csv";
                            
                using (TextReader readerComuni = new StreamReader(nomeFile))
                {
                    string line;
                    while ((line = readerComuni.ReadLine()) != null)
                    {
                        var fields = line.Split(';').ToArray();
                        var siglaProv = fields[13];

                        var denomProv = _provincie[siglaProv];

                        var comune = new Comune
                        {
                            //Codice Regione;Codice Città Metropolitana;Codice Provincia (1);
                            //Progressivo del Comune (2);
                            //Codice Comune formato alfanumerico;Denominazione in italiano;
                            //Denominazione in tedesco;Codice Ripartizione Geografica;Ripartizione geografica;Denominazione regione;
                            //Denominazione Città metropolitana;Denominazione provincia;Flag Comune capoluogo di provincia;Sigla automobilistica;
                            //Codice Comune formato numerico;Codice NomeComune numerico con 107 province (dal 2006 al 2009);
                            //Codice Comune numerico con 103 province (dal 1995 al 2005);Codice Catastale del comune;Popolazione legale 2011 (09/10/2011);Codice NUTS1 2010;Codice NUTS2 2010 (3) ;Codice NUTS3 2010;Codice NUTS1 2006;Codice NUTS2 2006 (3);Codice NUTS3 2006
                            CodiceRegione = fields[0],
                            CodiceCittàMetropolitana = fields[1],
                            CodiceProvincia = fields[2],
                            CodiceAlpha = fields[4],
                            Denominazione = fields[5],
                            NomeRegione = fields[9],
                            NomeCittàMetropolitana = fields[10],
                            DenominazioneProvincia = denomProv,
                            SiglaProvincia = fields[13],
                            FlagComuneCapoluogo = fields[12],
                            SiglaAuto = fields[13],
                            CodiceCatastale = fields[17],
                            CodiceComune = fields[14]
                        };

                        if (dictCap.ContainsKey(comune.Denominazione))
                        {
                            comune.Cap = dictCap[comune.Denominazione];
                        }
                        session.Insert(comune);
                    }
                    tx.Commit();
                }
            }
        }
    }
}
