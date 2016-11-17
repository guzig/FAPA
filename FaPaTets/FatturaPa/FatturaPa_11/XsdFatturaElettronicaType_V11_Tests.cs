using System;
using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    [TestFixture]
    public class XsdFatturaElettronicaType_V11_Tests
    {
        readonly string _schema = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Schemas\XSD_Schema_v11\fatturapa_v1.1.xsd";
        
        [Test]
        public void CanDeserializeAndSerializeAndValidateFatturaElettronica_V11_Type_11()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11001.xml";
            var ouPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) + @"\file.xml";

            var fatturaElettronicaTypeV11 = HelpersFaPA.GetFatturaPA( nomeFile );

            //SerializationHelpers.SerializeToDisk( ouPath, fatturaElettronicaTypeV11 );

            //TestHelpers.ValidateXmlFatturaFromDisk( ouPath, _schema);
        }

        [Test]
        public void CanValidateFatturaElettronica_V11_Type_1()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11001.xml";
            //var sch1 = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Schemas\XSD_Signature\xmldsig-core-schema.xsd";

            TestHelpers.ValidateXmlFatturaFromDisk(nomeFile, _schema );
        }

        [Test]
        public void CanDeserializeFatturaElettronica_V11_Type_1()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11001.xml";

            var fattura1 = HelpersFaPA.GetFatturaPA(nomeFile);

        }

        [Test]
        public void CanDeserializeFatturaElettronica_V11_Type_2()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11002.xml";
            var fattura1 = HelpersFaPA.GetFatturaPA( nomeFile );

        }

        [Test]
        public void CanDeserializeFatturaElettronica_V11_Type_3()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11003.xml";
            var fattura1 = HelpersFaPA.GetFatturaPA( nomeFile );

        }

        [Test]
        public void CanDeserializeFatturaElettronica_V11_Type_4()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11004.xml";
            var fattura1 = HelpersFaPA.GetFatturaPA( nomeFile );

        }
    }
}
