using System.IO;
using System.Text;
using System.Xml.Linq;
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
        private static readonly string FatturaPaXsdSchema = AppServices.StoreAccess.DataPath + @"\fatturapa_v1.1.xsd";
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

            Serializer = new XmlSerializer(typeof (FatturaElettronicaType), Overrides);
        }

        private static XmlSerializerNamespaces NameSpaceFatturaPa
        {
            get
            {
                if (_nameSpaceFatturaPa != null) return _nameSpaceFatturaPa;

                _nameSpaceFatturaPa = new XmlSerializerNamespaces();
                _nameSpaceFatturaPa.Add("p", "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1");

                return _nameSpaceFatturaPa;
            }
        }

        private static XmlSchemaSet FatturaPaXmlSchema
        {
            get
            {
                if (_fatturaPaXmlSchema != null) return _fatturaPaXmlSchema;

                _fatturaPaXmlSchema = new XmlSchemaSet();
                _fatturaPaXmlSchema.Add("http://www.fatturapa.gov.it/sdi/fatturapa/v1.1", FatturaPaXsdSchema);

                return _fatturaPaXmlSchema;
            } 
        }

        public static string ObjectToXml(FatturaElettronicaType objectInstance)
        {
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

        public static string ValidateFatturaPA(Fattura fattura)
        {
            var xmlData = ObjectToXml(fattura.FatturaPa);
            var document = XDocument.Parse(xmlData);
            var sb = new StringBuilder();
            document.Validate(FatturaPaXmlSchema, (o, e) =>
            {
                sb.Append(e.Message);
            });
            return sb.ToString();
        }

    }
}