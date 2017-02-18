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
        public void can_serialize_nested_proxies0()
        {
            var fattura = new Fattura();
            fattura.Init();
            var fattPa = fattura.FatturaPa;

            UtilsPA.FillFatturaPa( fattPa );

            // Proxing
            object proxied = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntity>( fattura );

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntity>( fattura );

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntity>( proxied );
            
            var f = ObjectExtensions.Copy( proxied );

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntity>( f );

        }


        [Test]
        public void can_serialize_nested_proxies()
        {
            var fattura = new Fattura();
            fattura.Init();
            var fattPa = fattura.FatturaPa;

            UtilsPA.FillFatturaPa( fattPa );

            // Proxing
            object proxied = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa );

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( fattPa );

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( proxied );


            //Unproxing
            var unProxy = ObjectExplorer.UnProxiedDeepCopy( proxied );

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( unProxy );

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( proxied );

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( fattPa );


            //Copying
            var copy = fattPa.CopyDeep();

            var origSer = SerializerHelpers.ObjectToXml( fattPa );
            var copySer = SerializerHelpers.ObjectToXml( copy );

            Assert.AreEqual( origSer, copySer );

            UtilsPA.CheckNestedRefEquals<FaPA.Core.BaseEntityFpa>( proxied, copy, false );

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntityFpa>( copy );

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( proxied );

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

            UtilsPA.FillFatturaPa( fattPa );

            #endregion

            object currentHeader = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa.FatturaElettronicaHeader );
            fattPa.FatturaElettronicaHeader = ( FaPA.Core.FaPa.FatturaElettronicaHeaderType ) currentHeader;

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( currentHeader );

            object currentBody = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa.FatturaElettronicaBody );
            fattPa.FatturaElettronicaBody = ( FaPA.Core.FaPa.FatturaElettronicaBodyType ) currentBody;

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( currentBody );

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

            UtilsPA.FillFatturaPa( fattPa );

            object current = ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( fattPa);

            UtilsPA.CheckAllTypesAreProxied<FaPA.Core.BaseEntityFpa>( current );

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
    }
}