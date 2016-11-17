using System;
using System.Data.SQLite;

namespace FaPaTets.DbSetUp
{
    public class SQLiteDatabaseScope : IDisposable
    {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger( typeof( SQLiteDatabaseScope ) );

        private object sync = new object();
        private readonly NHibernate.Cfg.Configuration _config;
        private readonly NHibernate.ISessionFactory _sessionFactory;
        private readonly string _initialDataFilename;
        private SQLiteConnection _connection;

        public SQLiteDatabaseScope( NHibernate.Cfg.Configuration configuration,
            NHibernate.ISessionFactory sessionFactory )
        {
            //Log.Info( "Creating database scope" );
            _config = configuration;
            _sessionFactory = sessionFactory;
        }

        public SQLiteDatabaseScope( NHibernate.Cfg.Configuration configuration, 
            NHibernate.ISessionFactory sessionFactory,
            string initialDataFilename )
            : this( configuration, sessionFactory )
        {
            _initialDataFilename = initialDataFilename;
        }

        public NHibernate.ISession OpenSession()
        {
            return _sessionFactory.OpenSession( GetConnection() );
        }

        public NHibernate.ISession OpenSession( NHibernate.IInterceptor interceptor )
        {
            return _sessionFactory.OpenSession( GetConnection(), interceptor );
        }

        public NHibernate.IStatelessSession OpenStatelessSession()
        {
            return _sessionFactory.OpenStatelessSession( GetConnection() );
        }

        private SQLiteConnection GetConnection()
        {
            if ( null == _connection )
                BuildConnection();
            return _connection;
        }

        private void BuildConnection()
        {
            //Log.Info( "Building SQLite database _connection" );
            _connection = new SQLiteConnection( CONNECTION_STRING );
            _connection.Open();
            BuildSchema();
            if ( !string.IsNullOrEmpty( _initialDataFilename ) )
                new SQLiteDataLoader( _connection, _initialDataFilename ).ImportData();
        }

        private void BuildSchema()
        {
            //Log.Debug( "Creating schema" );
            var se = new NHibernate.Tool.hbm2ddl.SchemaExport( _config );
            se.Execute( false, true, false, _connection, null );
        }

        private bool _disposedValue = false;

        private void Dispose( bool disposing )
        {
            if ( !this._disposedValue )
            {
                if ( disposing )
                {
                    //Log.Info( "Disposing database scope." );
                    if ( null != _connection )
                    {
                        _connection.Dispose();
                    }
                }
            }
            this._disposedValue = true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion
    }
}
