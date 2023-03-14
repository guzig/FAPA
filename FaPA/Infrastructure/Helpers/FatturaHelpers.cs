namespace FaPA.Infrastructure.Helpers{

    using System.Xml.Xsl;
    using System.Xml.Linq;

    public static class FatturaHelpers{

        private static XslCompiledTransform _transformerV11;
        private static XslCompiledTransform TransformerV11
        {

            get
            {
                if (_transformerV11 != null)
                {
                    return _transformerV11;
                }
                _transformerV11 = new XslCompiledTransform();
                _transformerV11.Load(@"C:\PAWAREDATA\EM\EMDATA\fatturapa_v1.1.xsl");
                return _transformerV11;
            }
        }

        private static XslCompiledTransform _transformerV121;
        private static XslCompiledTransform TransformerV121
        {

            get
            {
                if (_transformerV121 != null)
                {
                    return _transformerV121;
                }
                _transformerV121 = new XslCompiledTransform();
                _transformerV121.Load(@"C:\PAWAREDATA\EM\EMDATA\fatturaPA_v1.2.1.xsl");
                return _transformerV121;
            }
        }
        
        public static byte[] TransformXMLToPDF(string xml)
        {

            //var mydoc = XDocument.Load(inpXML);
            var mydoc = XDocument.Parse(xml);
            var newTree = new XDocument();

            var trasf = mydoc.ToString().Contains("versione=\"FPA12\"")
                ? TransformerV121
                : TransformerV11;

            lock (trasf)
            {

                using (var writer = newTree.CreateWriter())
                {
                    trasf.Transform(mydoc.CreateReader(), writer);
                    writer.Close();
                }
            }

            return (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(newTree.ToString());
        }
    }
}
