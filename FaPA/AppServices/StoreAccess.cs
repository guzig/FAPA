using System;
using FaPA.Properties;

namespace FaPA.AppServices
{
    public static class StoreAccess
    {
        private static readonly string SqlServerInstanceName = Settings.Default.SqlServerInstanceName;
        private static readonly string ServerName = Settings.Default.ServerName;
        private static readonly string DbSourcePath = ServerName + SqlServerInstanceName;
        public static readonly string DataPath = Settings.Default.LocalPath + Settings.Default.DataPath;
        public static readonly string BackUpPath = Settings.Default.LocalPath + Settings.Default.BackUpPath;
        public static readonly string DbFullPath = DataPath + @"\FEPA.MDF";
        public static readonly string DbLogFullPath = DataPath + @"\FEPA_Log.LOG";
        //public static readonly string StreamFullPath = DataPath + @"\STREAMS";

        static StoreAccess()
        { }
        
        private static readonly string Machine = Environment.MachineName;

        public static string ConnString { get; } = @"Data Source=" + SourcePath + ";Database=FEPA" + 
            ";User ID=" + User + ";Password=" + Password + ";";

        private static string _serverName;
        public static string SourcePath
        {
            get
            {
                if ( _serverName != null )
                    return _serverName;

                switch ( Machine )
                {
                    //case "T":
                    //case "TONIO":
                    case "CED":
                        _serverName = DbSourcePath;//"CED"; //"(local)";
                        break;
                    case "PC-DOMENICO":
                        _serverName = @"PC-DOMENICO\SQLEXPRESS";
                        break;
                    case "TRIBUTI1":
                        _serverName = @"TRIBUTI1\ENERGYMAN";
                        break;
                    default:
                        _serverName = DbSourcePath;
                        break;
                }

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

                switch ( Machine )
                {
                    //case "T":
                    case "TONIO":
                    case "PC-DOMENICO":
                        _password = "tonio";
                        break;
                    case "CED":
                        _password = "scintillaat20";
                        break;
                    case "TRIBUTI1":
                        _password = "zagot";
                        break;
                    default:
                        _password = "scintillaat20";
                        break;
                }

                return _password;
            }
        }

        public static string User
        {
            get
            {
                switch ( Machine )
                {
                    //case "CED":
                    //return "tonio";
                    default:
                        return "empwr";
                }
            }
        }
    }
}
