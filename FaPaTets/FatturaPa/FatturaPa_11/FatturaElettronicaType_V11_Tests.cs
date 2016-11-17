using System;
using System.IO;
using System.Xml.Serialization;
using FaPA.Core;
using FaPA.Core.FaPa;
using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    [TestFixture]
    public class FatturaElettronicaType_V11_Tests
    {
        readonly string _schema = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Schemas\XSD_Schema_v11\fatturapa_v1.1.xsd";

        [Test]
        public void CanDeserializeAndSerializeAndValidateFatturaElettronica_V11_Type_0()
        {
            var ouPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) + @"\file11111.xml";

            //var fattura = new Fattura();
            var fattPa =  new FaPA.Core.FaPa.FatturaElettronicaType();

            fattPa.FatturaElettronicaHeader = new FaPA.Core.FaPa.FatturaElettronicaHeaderType();
            fattPa.FatturaElettronicaHeader.CedentePrestatore = new FaPA.Core.FaPa.CedentePrestatoreType();

            var cedente = fattPa.FatturaElettronicaHeader.CedentePrestatore;

            cedente.DatiAnagrafici = new FaPA.Core.FaPa.DatiAnagraficiCedenteType();

            cedente.DatiAnagrafici.Anagrafica = new FaPA.Core.FaPa.AnagraficaType();

            var anagCedente = cedente.DatiAnagrafici.Anagrafica;

            cedente.DatiAnagrafici.CodiceFiscale = "1234567890123456";
            anagCedente.Denominazione = "Denominazione";
            cedente.Sede = new FaPA.Core.FaPa.IndirizzoType
            {
                CAP = "88051",
                Provincia = "CZ",
                Indirizzo = "indirizzo",
                NumeroCivico = "1c",
                Nazione = "IT"
            };

            //var committente = fattura.CessionarioCommittente;
            //var anagCommittente = fattura.AnagraficaCessionarioCommittente;
            //committente.DatiAnagrafici.CodiceFiscale = "1234567890123456";
            //anagCommittente.Denominazione = "Denominazione";
            //committente.Sede = new FaPA.Core.FaPa.IndirizzoType
            //{
            //    CAP = "88051",
            //    Provincia = "CZ",
            //    Indirizzo = "indirizzo",
            //    NumeroCivico = "1c",
            //    Nazione = "IT"
            //};

            //fattura.DatiGeneraliDocumento.Data = DateTime.Now;
            //fattura.DatiGeneraliDocumento.Numero = "1";

            var serializer = new XmlSerializer( typeof( FatturaElettronicaType ) );
            serializer.Serialize( File.Create( ouPath ), fattPa );

            //ValidateXmlFatturaFromDisk( ouPath, _schema );
        }


        [Test]
        public void CanDeserializeAndSerializeAndValidateFatturaElettronica_V11_Type_1()
        {
            var ouPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) + @"\file.xml";

            var fattura = new Fattura();
            var fattPa = fattura.FatturaPa = new FaPA.Core.FaPa.FatturaElettronicaType();

            var cedente = fattura.CedenteFornitore;
            var anagCedente = fattura.AnagraficaCedenteFornitore;
            cedente.DatiAnagrafici.CodiceFiscale = "1234567890123456";
            anagCedente.Denominazione = "Denominazione";
            cedente.Sede = new FaPA.Core.FaPa.IndirizzoType
            {
                CAP = "88051",
                Provincia = "CZ",
                Indirizzo = "indirizzo",
                NumeroCivico = "1c",
                Nazione = "IT"
            };

            var committente = fattura.CessionarioCommittente;
            var anagCommittente = fattura.AnagraficaCessionarioCommittente;
            committente.DatiAnagrafici.CodiceFiscale = "1234567890123456";
            //anagCommittente.Denominazione = "Denominazione";
            committente.Sede = new FaPA.Core.FaPa.IndirizzoType
            {
                CAP = "88051",
                Provincia = "CZ",
                Indirizzo = "indirizzo",
                NumeroCivico = "1c",
                Nazione = "IT"
            };

            fattura.DatiGeneraliDocumento.Data= DateTime.Now;
            fattura.DatiGeneraliDocumento.Numero = "1";

            var serializer = new XmlSerializer( typeof( FatturaElettronicaType ) );
            serializer.Serialize( File.Create( ouPath ), fattPa );

            //ValidateXmlFatturaFromDisk( ouPath, _schema );
        }

    }
}