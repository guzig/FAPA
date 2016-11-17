using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;

namespace FaPaTets.DbSetUp
{
    public class SQLiteDataLoader
    {

        //private static log4net.ILog Log = log4net.LogManager.GetLogger( typeof( SQLiteDataLoader ) );

        private const string ATTACHED_DB = "zxcvbnmInitialData";

        private SQLiteConnection connection;
        private string initialDataFilename;

        public SQLiteDataLoader( SQLiteConnection Connection,
            string InitialDataFilename )
        {
            connection = Connection;
            initialDataFilename = InitialDataFilename;
        }

        public void ImportData()
        {
            DataTable dt = connection.GetSchema( SQLiteMetaDataCollectionNames.Tables );
            var tableNames = ( from DataRow R in dt.Rows
                select ( string ) R["TABLE_NAME"] ).ToArray();
            AttachDatabase();
            foreach ( string tableName in tableNames )
            {
                CopyTableData( tableName );
            }
            DetachDatabase();
        }

        private void AttachDatabase()
        {
            SQLiteCommand cmd = new SQLiteCommand( connection );
            cmd.CommandText = String.Format( "ATTACH '{0}' AS {1}", initialDataFilename, ATTACHED_DB );
            //Log.Debug( cmd.CommandText );
            cmd.ExecuteNonQuery();
        }

        private void DetachDatabase()
        {
            SQLiteCommand cmd = new SQLiteCommand( connection );
            cmd.CommandText = string.Format( "DETACH {0}", ATTACHED_DB );
            //Log.Debug( cmd.CommandText );
            cmd.ExecuteNonQuery();
        }

        private void CopyTableData( string TableName )
        {
            int rowsAffected;
            SQLiteCommand cmd = new SQLiteCommand( connection );
            cmd.CommandText = string.Format( "INSERT INTO {0} SELECT * FROM {1}.{0}", TableName, ATTACHED_DB );
            //Log.Debug( cmd.CommandText );
            rowsAffected = cmd.ExecuteNonQuery();
            //Log.InfoFormat( "{0} {1} rows loaded", rowsAffected, TableName );
        }

    }
}