using System;
using System.ComponentModel;
using FaPaTets.DbSetUp;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.Data;
using NHibernate;
using NHibernate.Linq.Functions;
using NHibernate.Proxy.DynamicProxy;
using NUnit.Framework;

namespace FaPaTets.PersistanceTests
{
    [TestFixture]
    public class ProxyPersitanceTests : AbstractTestFixtureFixture
    {
        [Test]
        public void Created_proxy_entity_should_be_persistable()
        {
            var session = _sessionFactory.OpenSession(new AddPropertyChangedInterceptor()); //new AddPropertyChangedInterceptor()

            var fattura = DataTestFactory.GetFattura();

            AddPropChangedAndDataErrorInterceptorProxyFactory.Create( typeof(Committente), 
                fattura.AnagraficaCommittenteDB);

            AddPropChangedAndDataErrorInterceptorProxyFactory.Create(typeof(Fornitore),
                fattura.AnagraficaCedenteDB);

            fattura.SetTrasmittente();

            object current = fattura.FatturaPa;
            //ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>( ref current, "FaPA.Core" );
            fattura.FatturaPa = (FatturaElettronicaType) ObjectExplorer.UnProxiedAllInstances(current);
            using ( var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(typeof(Fornitore).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Committente).FullName, fattura.AnagraficaCommittenteDB);
                session.SaveOrUpdate( typeof( Fattura ).FullName, fattura );
                session.Flush();
                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                fattura.DatiGeneraliDocumento.Data = DateTime.Now.AddDays(1);
                fattura.TotaleFatturaDB = 101;
                session.Update(fattura);
                session.Flush();
                transaction.Commit();
            }

            Fattura read;
            using (var tx = session.BeginTransaction())
            {
                session.Evict(session.Get<Fattura>(fattura.Id));
                read = session.Get<Fattura>(fattura.Id);
                tx.Commit();
            }

            Assert.IsInstanceOf<IProxy>(read);

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
                session.SaveOrUpdate(typeof(Fornitore).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Committente).FullName, fattura.AnagraficaCommittenteDB);
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