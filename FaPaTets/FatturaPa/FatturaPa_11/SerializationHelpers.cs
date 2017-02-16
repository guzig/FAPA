using System.IO;
using System.Xml.Serialization;
using FaPA.Core;
using FaPA.Core.FaPa;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    public class SerializationHelpers
    {
        public static void SerializeToDisk( string ouPath, FatturaElettronicaType fatturaElettronicaTypeV11 )
        {
            var xmlAttributes = new XmlAttributes() { XmlIgnore = true };
            var overrides = new XmlAttributeOverrides();
            overrides.Add( typeof( BaseEntity ), "Id", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "DomainResult", xmlAttributes );
            overrides.Add( typeof( Fattura ), "DomainResult", xmlAttributes );
            overrides.Add( typeof( BaseEntityFpa ), "DomainResult", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "Version", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "IsValidating", xmlAttributes );
            overrides.Add( typeof( BaseEntityFpa ), "IsValidating", xmlAttributes );
            overrides.Add( typeof( BaseEntity ), "IsNotyfing", xmlAttributes );
            overrides.Add( typeof( BaseEntityFpa ), "IsNotyfing", xmlAttributes );
            var serializer = new XmlSerializer( typeof ( FatturaElettronicaType ), overrides );
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add( "p", "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2" );
            //ns.Add( "xsi","" );
            //ns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            //ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            //var fileStream = File.Create( ouPath  );

            //using (TextWriter writer = new Utf8StringWriter())
            //{
            //    serializer.Serialize(writer, fatturaElettronicaTypeV11, ns);
            //    writer.Write(ouPath);
            //    writer.Close();
            //}

            TextWriter writer = null;
            try
            {
                //var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(ouPath, false);
                serializer.Serialize(writer, fatturaElettronicaTypeV11, ns);
            }
            finally
            {
                writer?.Close();
            }

            //serializer.Serialize( fileStream, fatturaElettronicaTypeV11, ns );
            //fileStream.Dispose();
        }
    }
}