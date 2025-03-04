using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.DomainServices;
using FaPA.Infrastructure;
using NUnit.Framework;

namespace FaPaTets.PersistanceTests
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

            var list = ObjectExplorer.FindAllInstancesDeep<object>(fattura);

            foreach (var coreValidator in list)
            {
                var b = coreValidator.GetType().BaseType;

                CoreValidatorService.GetValidationErrors( "ImportoPagamento", coreValidator );

                //CoreValidatorService.GetValidator(b).GetValidationErrors(coreValidator);
            }


        }
    }
}