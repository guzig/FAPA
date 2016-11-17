using System.IO;
using System.Xml.Serialization;
using FaPA.Core.FaPa;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    public class SerializationHelpers
    {
        public static void SerializeToDisk( string ouPath, FatturaElettronicaType fatturaElettronicaTypeV11 )
        {
            var serializer = new XmlSerializer( typeof ( FatturaElettronicaType ) );
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add( "p", "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" );
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