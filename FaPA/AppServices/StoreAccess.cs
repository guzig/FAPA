using System.Data.SqlClient;
using System.IO;
using FaPA.Infrastructure.Utils;
using FaPA.Properties;

namespace FaPA.AppServices
{
    public static class StoreAccess
    {
        private static readonly string SqlServerInstanceName = Settings.Default.SqlServerInstanceName;

        private static readonly string ServerName = string.IsNullOrWhiteSpace(Settings.Default.ServerName) || 
            Settings.Default.ServerName.ToLower().Contains( "local" ) ? 
            System.Environment.MachineName : Settings.Default.ServerName;

        private static readonly string DbSourcePath = Path.Combine(ServerName, SqlServerInstanceName);
        public static readonly string DataPath = Path.Combine(Settings.Default.LocalPath, Settings.Default.DataPath);
        public static readonly string BackUpPath = Path.Combine(Settings.Default.LocalPath, Settings.Default.BackUpPath);
        public static readonly string DbFullPath = Path.Combine(DataPath, "FEPA.MDF");
        public static readonly string DbLogFullPath = Path.Combine(DataPath, "FEPA_Log.LOG");
        //public static readonly string StreamFullPath = DataPath + @"\STREAMS";


        public static readonly string FatturaPaXsdSchema = DataPath + @"\fatturapa_v1.2.xsd";

        public const string FatturaPaXslPaSchema = "fatturapa_v1.2.1.xsl";
        public const string FatturaPaXslOrdSchema = "fatturaordinaria_v1.2.1.xsl";

        public static readonly string FatturaPaXslPaSchemaPath = Path.Combine( DataPath, FatturaPaXslPaSchema );
        public static readonly string FatturaPaXslOrdSchemaPath = Path.Combine( DataPath, FatturaPaXslOrdSchema );

        static StoreAccess()
        { }
        
        //private static readonly string Machine = Environment.MachineName;

        public static string ConnString { get; } = @"Data Source=" + SourcePath + ";Database=FEPA" + 
            ";User ID=" + User + ";Password=" + Password + ";";

        private static string _serverName;
        public static string SourcePath
        {
            get
            {
                if ( _serverName != null )
                    return _serverName;

                _serverName = DbSourcePath;

                return _serverName;
            }
        }

        private static string _password;
        public static string Password
        {
            get
            {
                if ( _password != null )
                    return _password;

                _password = "scintillaat20!";

                return _password;
            }
        }

        public static string User
        {
            get
            {
                return "empwr";
            }
        }

        /// <summary>
        /// Test that the server is connected
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <returns>true if the connection is opened</returns>
        public static bool IsServerConnected()
        {
            ShowCursor.Show();
            try
            {
                using ( var connection = new SqlConnection( ConnString ) )
                {
                    connection.Open();
                    return true;
                }
            }
            catch ( SqlException )
            {
                return false;
            }
        }
    }
}
