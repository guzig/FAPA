using System;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    public class TestHelpers
    {
        private static void Validate(string sch2, XDocument document)
        {
            var schemas = new XmlSchemaSet();
            //schemas.Add( "http://www.w3.org/2000/09/xmldsig#", sch1 );
            schemas.Add("http://www.fatturapa.gov.it/sdi/fatturapa/v1.1", sch2);

            bool errors = false;
            document.Validate(schemas, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errors = true;
            });
            Console.WriteLine("document {0}", errors ? "did not validate" : "validated");
        }

        public static void ValidateXmlFatturaFromDisk( string nomeFile, string sch2 )
        {
            XDocument document = XDocument.Load( nomeFile );
            Validate(sch2, document);
        }
        
        public static void ValidateXmlFattura(string xml, string sch2 )
        {
            var document = XDocument.Parse( xml ) ;
            Validate(sch2, document);
        }


    }
}