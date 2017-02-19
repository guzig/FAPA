using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using FaPA.AppServices;
using NUnit.Framework;

namespace FaPaTets.Misc
{
    class XmlStuff
    {
        [Test]
        public void ApplyXlsTrasfOnDisk(  )
        {
            var myXslTransform = new XslTransform();
            myXslTransform.Load( StoreAccess.FatturaPaXslPaSchemaPath );
            string xmlPath= "ITGZZMRA65R50D181Y_00002.xml";
            var outputfile = "trs" + xmlPath;
            myXslTransform.Transform( xmlPath, outputfile );

        }

        //public virtual string ApplyXlsTrasfInMemory( string input)
        //{
        //    if ( DatiTrasmissione?.FormatoTrasmissione == null ) return null;

        //    var xslPath = DatiTrasmissione.FormatoTrasmissione == FormatoTrasmissioneType.FPA12
        //        ? StoreAccess.FatturaPaXslPaSchema : StoreAccess.FatturaPaXslOrdSchema;

        //    string output;
        //    using ( StringReader sReader = new StringReader( input ) )
        //    using ( XmlReader xReader = XmlReader.Create( sReader ) )
        //    using ( StringWriter sWriter = new StringWriter() )
        //    using ( XmlWriter xWriter = XmlWriter.Create( sWriter ) )
        //    {
        //        XslCompiledTransform xslt = new XslCompiledTransform();
        //        xslt.Load( xslPath );
        //        xslt.Transform( xReader, xWriter );
        //        output = sWriter.ToString();
        //    }

        //    return output;
        //}
    }
}
