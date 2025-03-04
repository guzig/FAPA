using System;
using System.IO;
using FaPA.Core;
using FaPA.DomainServices.Helpers;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace FaPaTets.DbSetUp
{
    public class RepoTest
    {
        [SetUp]
        public void CreateSchema()
        {
            DeleteDatabaseIfExists();

            var schemaUpdate = new SchemaUpdate( NHibernateHelper.Configuration );
            schemaUpdate.Execute( Console.WriteLine, true );


        }

        [Test]
        public void CanSavePerson()
        {
            
            using ( ISession session = NHibernateHelper.OpenSession() )
            using ( ITransaction transaction = session.BeginTransaction() )
            {
                session.Save( new Fattura() {Id=1} );
                transaction.Commit();
            }
            //Assert.AreEqual( 1, _personRepo.RowCount() );
        }

        [TearDown]
        public void DeleteDatabaseIfExists()
        {
            if ( File.Exists( "FaPAtest.db" ) )
                File.Delete( "FaPAtest.db" );
        }
    }
}