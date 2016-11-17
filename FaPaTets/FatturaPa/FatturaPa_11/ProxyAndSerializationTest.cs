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

            object currentHeader = fattPa.FatturaElettronicaHeader;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>( ref currentHeader, "FaPA.Core" );
            fattPa.FatturaElettronicaHeader = ( FaPA.Core.FaPa.FatturaElettronicaHeaderType ) currentHeader;

            CheckAllTypesAreProxied( currentHeader );

            object currentBody = fattPa.FatturaElettronicaBody;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>( ref currentBody, "FaPA.Core" );
            fattPa.FatturaElettronicaBody = ( FaPA.Core.FaPa.FatturaElettronicaBodyType ) currentBody;

            CheckAllTypesAreProxied( currentBody );

            var xml = fattura.GetXmlDocument();

            Assert.IsNotNull( xml );

            Assert.AreEqual( "00000SDI11RF01PortoBelloITTP03MP010", xml.InnerText );

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

            object currentHeader = fattPa.FatturaElettronicaHeader;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>( ref currentHeader, "FaPA.Core" );
            fattPa.FatturaElettronicaHeader = ( FaPA.Core.FaPa.FatturaElettronicaHeaderType ) currentHeader;

            CheckAllTypesAreProxied( currentHeader );

            object currentBody = fattPa.FatturaElettronicaBody;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>( ref currentBody, "FaPA.Core" );
            fattPa.FatturaElettronicaBody = ( FaPA.Core.FaPa.FatturaElettronicaBodyType ) currentBody;

            CheckAllTypesAreProxied( currentBody );

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

            object current = fattPa;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>(ref current, "FaPA.Core");

            CheckAllTypesAreProxied( current );

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



        private static void CheckAllTypesAreProxied( object current )
        {
            var instances = ObjectExplorer.FindAllInstances<FaPA.Core.BaseEntityFpa>( current ).ToArray();

            foreach ( var instance in instances )
            {
                Assert.AreEqual( true, instance.GetType().Name.EndsWith( "Proxy" ) );
            }
        }

        private static void FillFatturaPa( FatturaElettronicaType fattPa )
        {
            fattPa.FatturaElettronicaHeader = new FaPA.Core.FaPa.FatturaElettronicaHeaderType();
            fattPa.FatturaElettronicaHeader.CedentePrestatore = new FaPA.Core.FaPa.CedentePrestatoreType();
            fattPa.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici =
                new FaPA.Core.FaPa.DatiAnagraficiCedenteType();
            fattPa.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.Anagrafica =
                new FaPA.Core.FaPa.AnagraficaType();
            fattPa.FatturaElettronicaHeader.CedentePrestatore.Sede =
                new FaPA.Core.FaPa.IndirizzoType() { Comune = "PortoBello" };
            fattPa.FatturaElettronicaBody = new FaPA.Core.FaPa.FatturaElettronicaBodyType();
            fattPa.FatturaElettronicaBody.DatiPagamento = new[ ]
            {
                new FaPA.Core.FaPa.DatiPagamentoType() { CondizioniPagamento = FaPA.Core.FaPa.CondizioniPagamentoType.TP03 }
            };
        }
    }
}