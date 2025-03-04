using System;
using FaPaTets.DbSetUp;
using FaPaTets.FatturaPa.FatturaPa_11;
using FaPA.Core;
using FaPA.Data;
using FaPA.DomainServices.Helpers;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Proxy.DynamicProxy;
using NUnit.Framework;

namespace FaPaTets.PersistanceTests
{
    [TestFixture]
    public class ProxyPersitanceTests : AbstractTestFixtureFixture
    {
        [Test]
        public void can_serialize_nested_proxies0()
        {
            var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ); //new AddPropertyChangedInterceptor()

            Anagrafica fattura;
            using ( var transaction = session.BeginTransaction() )
            {
                fattura = session.Get<Anagrafica>(32768L);
                transaction.Commit();
            }


            // Proxing
            var unproxy = fattura.Unproxy();

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntity>( unproxy );

            //object proxied = ObjectExtensions1.Copy( unproxy );

            //CheckAllTypesAreProxied<FaPA.Core.BaseEntity>( proxied );

            //var f = ObjectExtensions.Copy( proxied );

            //CheckAllTypesAreProxied<FaPA.Core.BaseEntity>( f );

        }


        [Test]
        public void Created_proxy_entity_should_be_persistable()
        {
            var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ); //new AddPropertyChangedInterceptor()

            var fattura = DataTestFactory.GetFattura();
            fattura.Init();

            fattura.SyncFatturaPa();

            UtilsPA.FillFatturaPa( fattura.FatturaPa );

            using ( var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCommittenteDB);
                session.SaveOrUpdate( typeof( Fattura ).FullName, fattura );
                session.Flush();
                transaction.Commit();
            }

            //var other = ( Fattura ) ObjectExplorer.UnProxiedDeepCopy( fattura );

            //var other = ( Fattura ) ObjectExtensions1.Copy( fattura );

            Fattura read;
            using (var tx = session.BeginTransaction())
            {
                session.Evict(session.Get<Fattura>(fattura.Id));
                read = session.Get<Fattura>(fattura.Id);
                tx.Commit();
            }

            Assert.IsInstanceOf<IProxy>(read);

            using ( var transaction = session.BeginTransaction() )
            {
                read.DatiGeneraliDocumento.Data = DateTime.Now.AddDays( 10 );
                read.TotaleFatturaDB = 101;
                session.Update( read );
                session.Flush();
                transaction.Commit();
            }


            Assert.AreEqual(fattura.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdCodice,
                            read.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdCodice);
        }


        [Test]
        public void Created_proxy_entity_should_handle_PropertyChanged()
        {
            var fattura = DataTestFactory.GetFattura();

            using (var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ) )
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCommittenteDB);
                session.SaveOrUpdate(typeof(Fattura).FullName, fattura);
                session.Flush();
                transaction.Commit();
            }

            Fattura fatturaInDb;
            using (var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor()))
            using (var transaction = session.BeginTransaction())
            {
                fatturaInDb = session.Get<Fattura>( fattura.Id );
                transaction.Commit();
            }


            Check_entity_handles_PropertyChanged(fatturaInDb);
        }

        [Test]
        public void Created_proxy_entity_should_handle_PropertyChanged1()
        {
            Fattura fatturaInDb;
            
            using ( var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ) )
            using ( var transaction = session.BeginTransaction() )
            {
                session.FlushMode = FlushMode.Never;
                fatturaInDb = session.Get<Fattura>( 131072L );
                transaction.Commit();
            }
                
            Assert.IsInstanceOf<IProxy>( fatturaInDb.FatturaPa.FatturaElettronicaHeader );
            //Assert.That( eventWasCalled );
            //Assert.That( propertyName, Is.EqualTo( "Id" ) );
            //Assert.That( sender, Is.SameAs( fatturaInDb.FatturaPa ) );
        }

    }

    
}