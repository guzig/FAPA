using System;
using FaPaTets.DbSetUp;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Data;
using NUnit.Framework;

namespace FaPaTets.PersistanceTests
{
    [TestFixture]
    public class ProxyPersitanceTests : AbstractTestFixtureFixture
    {
        [Test]
        public void Created_proxy_entity_should_be_persistable()
        {
            var session = _sessionFactory.OpenSession(); //new AddPropertyChangedInterceptor()

            var fattura = DataTestFactory.GetFattura();

            AddPropChangedAndDataErrorInterceptorProxyFactory.Create( typeof(Committente), 
                fattura.AnagraficaCommittenteDB);

            AddPropChangedAndDataErrorInterceptorProxyFactory.Create(typeof(Fornitore),
                fattura.AnagraficaCedenteDB);

            //AddPropChangedAndDataErrorInterceptorProxyFactory.Create(typeof(Fattura), fattura);

            object current = fattura.FatturaPa.FatturaElettronicaHeader;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.FaPa.FatturaElettronicaHeaderType>( ref current, "FaPA.Core" );
            
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
        }

        [Test]
        public void Created_proxy_entity_should_handle_PropertyChanged()
        {
            var fattura = DataTestFactory.GetFattura();

            using (var session = _sessionFactory.OpenSession(new AddPropertyChangedInterceptor() ) )
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(typeof(Fornitore).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Committente).FullName, fattura.AnagraficaCommittenteDB);
                session.SaveOrUpdate(typeof(Fattura).FullName, fattura);
                session.Flush();
                transaction.Commit();
            }


            Fattura fatturaInDb;
            using (var session = _sessionFactory.OpenSession(new AddPropertyChangedInterceptor()))
            using (var transaction = session.BeginTransaction())
            {
                fatturaInDb = session.Get<Fattura>( fattura.Id );
                transaction.Commit();
            }


            Check_entity_handles_PropertyChanged(fatturaInDb);
        }

    }
}