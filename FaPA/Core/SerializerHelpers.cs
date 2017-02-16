using System.Globalization;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;
using FaPA.Core.FaPa;
using FaPA.DomainServices.Utils;

namespace FaPA.Core
{
    public static class SerializerHelpers
    {
        private static readonly XmlAttributes XmlAttributes = new XmlAttributes() { XmlIgnore = true };
        private static readonly XmlAttributeOverrides Overrides = new XmlAttributeOverrides();
        private static readonly string FatturaPaXsdSchema = AppServices.StoreAccess.DataPath + @"\fatturapa_v1.2.xsd";
        private static readonly XmlSerializer Serializer ;
        private static XmlSerializerNamespaces _nameSpaceFatturaPa;
        private static XmlSchemaSet _fatturaPaXmlSchema;

        //ctor
        static SerializerHelpers()
        {
            Overrides.Add(typeof(BaseEntity), "Id", XmlAttributes);
            Overrides.Add(typeof(BaseEntity), "DomainResult", XmlAttributes);
            Overrides.Add(typeof(Fattura), "DomainResult", XmlAttributes);
            Overrides.Add(typeof(BaseEntityFpa), "DomainResult", XmlAttributes);
            Overrides.Add(typeof(BaseEntity), "Version", XmlAttributes);
            Overrides.Add(typeof(BaseEntity), "IsValidating", XmlAttributes);
            Overrides.Add(typeof(BaseEntityFpa), "IsValidating", XmlAttributes);
            Overrides.Add(typeof(BaseEntity), "IsNotyfing", XmlAttributes);
            Overrides.Add(typeof(BaseEntityFpa), "IsNotyfing", XmlAttributes);

            //ObjectExplorer.OverridesAllInstances( typeof( FatturaElettronicaType ), Overrides );

            //Serializer = new XmlSerializer( typeof( FatturaElettronicaType ), Overrides, new Type[]
            //{
            //    AddPropChangedAndDataErrorInterceptorProxyFactory.Create( 
            //        typeof( FatturaElettronicaType ), new FatturaElettronicaType() ).GetType()
            //}, null, "" );

            Serializer = new XmlSerializer( typeof( FatturaElettronicaType ), Overrides );

        }

        private static XmlSerializerNamespaces NameSpaceFatturaPa
        {
            get
            {
                if (_nameSpaceFatturaPa != null) return _nameSpaceFatturaPa;

                _nameSpaceFatturaPa = new XmlSerializerNamespaces();
                _nameSpaceFatturaPa.Add("p", "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2" );
                _nameSpaceFatturaPa.Add( "ds", "http://www.w3.org/2000/09/xmldsig#" );
                _nameSpaceFatturaPa.Add( "xsi", "http://www.w3.org/2001/XMLSchema-instance" );

                return _nameSpaceFatturaPa;
            }
        }

        public static XmlSchemaSet FatturaPaXmlSchema
        {
            get
            {
                if (_fatturaPaXmlSchema != null) return _fatturaPaXmlSchema;

                _fatturaPaXmlSchema = new XmlSchemaSet();
                _fatturaPaXmlSchema.Add( "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2", FatturaPaXsdSchema);

                return _fatturaPaXmlSchema;
            } 
        }

        public static string ObjectToXml(FatturaElettronicaType objectInstance)
        {
            var customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            string utf8;
            using ( StringWriter writer = new Utf8StringWriter() )
            {
                Serializer.Serialize(writer, objectInstance, NameSpaceFatturaPa);                   
                utf8 = writer.ToString();
            }
            return utf8;
        }
        
        public static FatturaElettronicaType XmlToObject(string objectData)
        {
            FatturaElettronicaType result;
            using ( TextReader reader = new StringReader(objectData) )
            {
                result = (FatturaElettronicaType)Serializer.Deserialize(reader);
            }
            return result;
        }



    }
}