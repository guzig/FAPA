using System;
using FaPA.AppServices.CoreValidation;
using FaPA.DomainServices;
using FaPA.Infrastructure;
using FaPA.Core;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;

namespace FaPaTets.DbSetUp
{
    public class ValidationTests
    {
        [Test]
        public void CanValidateFattura()
        {
            BootStrapper.Initialize();
            var session = BootStrapper.SessionFactory.OpenSession();

            Fattura fattura;
            using (var tx = session.BeginTransaction())
            {
                fattura = session.Get<Fattura>(98304L);
                tx.Commit();
            }

            var list = ObjectExplorer.FindAllInstances<object>(fattura);

            foreach (var coreValidator in list)
            {
                var b = coreValidator.GetType().BaseType;

                CoreValidatorService.GetValidationErrors( "ImportoPagamento", coreValidator );

                //CoreValidatorService.GetValidator(b).GetValidationErrors(coreValidator);
            }


        }
    }

    public class QueriesTest
    {
        [Test]
        public void CanValidateUniqueFornitoreFattura()
        {
            BootStrapper.Initialize();
            var session = BootStrapper.SessionFactory.OpenSession();
            int result;

            var fattura = new Fattura();
            fattura.Init();
            fattura.DatiGeneraliDocumento.Data = DateTime.Now;
            fattura.DatiGeneraliDocumento.Numero = "a1";
            using (var tx = session.BeginTransaction())
            {
                fattura.AnagraficaCedenteDB = session.Get<Fornitore>(32768L);
                tx.Commit();
            }

            for (int i = 0; i < 5; i++)
            {
                result = IsUniqueFattura(session, fattura);
            }
            
        }

        private static int IsUniqueFattura(ISession session, Fattura fattura)
        {
            int result;
            using (var tx = session.BeginTransaction())
            {
                result = NHibernateStaticContainer.Session.QueryOver<Fattura>().
                    Where(f => f.DataFatturaDB == fattura.DatiGeneraliDocumento.Data).
                    And(f => f.NumeroFatturaDB == fattura.DatiGeneraliDocumento.Numero).
                    And(f => f.AnagraficaCedenteDB.Id == fattura.AnagraficaCedenteDB.Id).
                    And(f => f.Id != fattura.Id).
                    Select(Projections.Count<Fattura>(f => f.Id)).Cacheable().CacheMode(CacheMode.Normal)
                    .FutureValue<int>().Value;

                //var criteria = Session.CreateCriteria(typeof(Fattura));
                //criteria.Add(Restrictions.Eq("DataFatturaDB", fattura.DataFatturaDB));
                //criteria.Add(Restrictions.Eq("NumFattura", fattura.NumFattura));
                //criteria.Add(Restrictions.Not(Restrictions.IdEq(fattura.Id)));
                //criteria.SetProjection(Projections.Count(Projections.Id()));
                //criteria.SetCacheMode(CacheMode.Normal);
                //criteria.SetCacheable(true);
                //var result = criteria.UniqueResult<int>() == 0;

                tx.Commit();
            }
            return result;
        }
    }
}
