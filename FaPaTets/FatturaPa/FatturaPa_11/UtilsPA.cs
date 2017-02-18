using FaPA.AppServices.CoreValidation;
using FaPA.Core.FaPa;
using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    public static class UtilsPA
    {
        public static void CheckNestedRefEquals<T>( object orig, object copy, bool condition ) where T : class
        {
            var instances = ObjectExplorer.FindAllInstancesDeep<T>( orig ).ToArray();
            var others = ObjectExplorer.FindAllInstancesDeep<T>( copy ).ToArray();

            for ( int index = 0; index < instances.Length; index++ )
            {
                var instance = instances[index];
                var other = others[index];
                Assert.AreEqual( condition, ReferenceEquals( instance, other ) );
            }
        }

        public static void CheckAllTypesAreProxied<T>( object current ) where T : class
        {
            var instances = ObjectExplorer.FindAllInstancesDeep<T>( current ).ToArray();

            foreach ( var instance in instances )
            {
                Assert.AreEqual( true, instance.GetType().Name.EndsWith( "Proxy" ) );
            }
        }

        public static void CheckAllTypesAreUnProxied<T>(object current) where T : class 
        {
            var instances = ObjectExplorer.FindAllInstancesDeep<T>(current).ToArray();

            foreach (var instance in instances)
            {
                Assert.AreEqual(false, instance.GetType().Name.EndsWith("Proxy"));
            }
        }

        public static void FillFatturaPa( FatturaElettronicaType fattPa )
        {
            fattPa.FatturaElettronicaHeader.CedentePrestatore = new FaPA.Core.FaPa.CedentePrestatoreType();

            fattPa.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici =
                new FaPA.Core.FaPa.DatiAnagraficiCedenteType();

            fattPa.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.Anagrafica =
                new FaPA.Core.FaPa.AnagraficaType();
            fattPa.FatturaElettronicaHeader.CedentePrestatore.Sede =
                new FaPA.Core.FaPa.IndirizzoType() { Comune = "PortoBello" };
            fattPa.FatturaElettronicaBody = new FaPA.Core.FaPa.FatturaElettronicaBodyType()
            {
                DatiGenerali = new FaPA.Core.FaPa.DatiGeneraliType()
                {
                    DatiGeneraliDocumento = new FaPA.Core.FaPa.DatiGeneraliDocumentoType()
                }, DatiBeniServizi = new FaPA.Core.FaPa.DatiBeniServiziType()


            };
            fattPa.FatturaElettronicaBody.DatiPagamento = new[ ]
            {
                new FaPA.Core.FaPa.DatiPagamentoType() { CondizioniPagamento = FaPA.Core.FaPa.CondizioniPagamentoType.TP03 }
            };
        }
    }
}