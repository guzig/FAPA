using System;
using System.IO;
using System.Xml.Serialization;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.DomainServices.Utils;
using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    public class ProxyAndSerializationTest 
    {
        [Test]
        public void can_serialize_nested_proxies()
        {
            var fattura = new Fattura();
            fattura.Init();
            var fattPa = fattura.FatturaPa;

            FillFatturaPa( fattPa );

            // Proxing
            object proxied = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa );

            CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( fattPa );

            CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( proxied );


            //Unproxing
            var unProxy = ObjectExplorer.UnProxiedDeepCopy( proxied );

            CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( unProxy );

            CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( proxied );

            CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( fattPa );


            //Copying
            var copy = fattPa.CopyDeep();

            var origSer = SerializerHelpers.ObjectToXml( fattPa );
            var copySer = SerializerHelpers.ObjectToXml( copy );

            Assert.AreEqual( origSer, copySer );

            CheckNestedRefEquals<FaPA.Core.BaseEntityFpa>( proxied, copy, false );

            CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( copy );

            CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( proxied );

            var xml = fattura.GetXmlFatturaPA();

            Assert.IsNotNull( xml );

            Assert.AreEqual( "RF01PortoBelloITCCTD010001-01-01TP03MP010", xml.InnerText );

            Console.WriteLine( xml.InnerXml );
        }

        [Test]
        public void can_serialize_nested_proxies_1()
        {
            var fattura = new Fattura();
            fattura.Init();
            var fattPa = fattura.FatturaPa;
            //new FaPA.Core.FaPa.FatturaElettronicaType();

            #region 

            FillFatturaPa( fattPa );

            #endregion

            object currentHeader = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa.FatturaElettronicaHeader );
            fattPa.FatturaElettronicaHeader = ( FaPA.Core.FaPa.FatturaElettronicaHeaderType ) currentHeader;

            CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( currentHeader );

            object currentBody = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa.FatturaElettronicaBody );
            fattPa.FatturaElettronicaBody = ( FaPA.Core.FaPa.FatturaElettronicaBodyType ) currentBody;

            CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( currentBody );

            var nameSpaceFatturaPa = new XmlSerializerNamespaces();
            nameSpaceFatturaPa.Add( "p", "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" );

            var overrides = new XmlAttributeOverrides();
            var xmlAttributes = new XmlAttributes() { XmlIgnore = true };
            overrides.Add( typeof( BaseEntity ), "Id", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "DomainResult", xmlAttributes );
            overrides.Add( typeof( Fattura ), "DomainResult", xmlAttributes );
            overrides.Add( typeof( BaseEntityFpa ), "DomainResult", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "Version", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "IsValidating", xmlAttributes );
            overrides.Add( typeof( BaseEntityFpa ), "IsValidating", xmlAttributes );

            ObjectExplorer.OverridesAllInstances( typeof( FatturaElettronicaType ), overrides );

            var serializer = new XmlSerializer( typeof( FatturaElettronicaType ), overrides );

            var utf8 = string.Empty;
            using ( StringWriter writer = new Utf8StringWriter() )
            {
                serializer.Serialize( writer, fattura.FatturaPa, nameSpaceFatturaPa );
                utf8 = writer.ToString();
                Console.WriteLine( utf8 );
            }

            FatturaElettronicaType result = null;
            using ( TextReader reader = new StringReader( utf8 ) )
            {
                result = ( FatturaElettronicaType ) serializer.Deserialize( reader );
            }

            Assert.IsNotNull( result );

        }

        [Test]
        public void can_serialize_nested_proxies_0()
        {
            var fattura = new Fattura();
            fattura.Init();
            var fattPa = fattura.FatturaPa;
            //new FaPA.Core.FaPa.FatturaElettronicaType();

            FillFatturaPa( fattPa );

            object current = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa);

            CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( current );

            var nameSpaceFatturaPa = new XmlSerializerNamespaces();
            nameSpaceFatturaPa.Add("p", "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1");

            var overrides = new XmlAttributeOverrides();
            var xmlAttributes = new XmlAttributes() { XmlIgnore = true };
            overrides.Add(typeof(BaseEntity), "Id", xmlAttributes);
            overrides.Add(typeof(BaseEntity), "DomainResult", xmlAttributes);
            overrides.Add(typeof(Fattura), "DomainResult", xmlAttributes);
            overrides.Add(typeof(BaseEntityFpa), "DomainResult", xmlAttributes);
            overrides.Add(typeof(BaseEntity), "Version", xmlAttributes);
            overrides.Add(typeof(BaseEntity), "IsValidating", xmlAttributes);
            overrides.Add(typeof(BaseEntityFpa), "IsValidating", xmlAttributes);

            ObjectExplorer.OverridesAllInstances( typeof ( FatturaElettronicaType ), overrides );
            
            var serializer = new XmlSerializer( current.GetType(), overrides);
            
            var utf8=string.Empty;
            using (StringWriter writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, current, nameSpaceFatturaPa);
                utf8 = writer.ToString();
                Console.WriteLine( utf8 );
            }

            FatturaElettronicaType result = null;
            using (TextReader reader = new StringReader(utf8))
            {
                result = (FatturaElettronicaType)serializer.Deserialize(reader);
            }
            
            Assert.IsNotNull( result );

        }

        private static void CheckNestedRefEquals<T>( object orig, object copy, bool condition ) where T : class
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

        private static void CheckAllTypesAreProxied<T>( object current ) where T : class
        {
            var instances = ObjectExplorer.FindAllInstancesDeep<T>( current ).ToArray();

            foreach ( var instance in instances )
            {
                Assert.AreEqual( true, instance.GetType().Name.EndsWith( "Proxy" ) );
            }
        }

        private static void CheckAllTypesAreUnProxied<T>(object current) where T : class 
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