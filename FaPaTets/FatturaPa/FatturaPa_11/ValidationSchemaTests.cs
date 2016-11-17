using System;
using FaPA.Core.FaPa;
using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    //http://stackoverflow.com/questions/28571394/serialize-part-of-xml-file-want-namespace-on-root-not-on-serialized-subelement

    public class ValidationSchemaTests
    {
        private readonly string _schema = FaPA.AppServices.StoreAccess.DataPath + @"\fatturapa_v1.1.xsd";
        //TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Schemas\XSD_Schema_v11\fatturapa_v1.1.xsd";

        [Test]
        public void ValidateFatturaElettronica_V11_Test()
        {
            //var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11001.xml";
            //var ouPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) + @"\file.xml";

            var fatturaElettronicaTypeV11 = new FatturaElettronicaType();
            fatturaElettronicaTypeV11.FatturaElettronicaHeader = new FaPA.Core.FaPa.FatturaElettronicaHeaderType
            {
                DatiTrasmissione = new FaPA.Core.FaPa.DatiTrasmissioneType
                {
                    CodiceDestinatario = "AAAAAA",
                    FormatoTrasmissione = new FaPA.Core.FaPa.FormatoTrasmissioneType(),
                    ProgressivoInvio = "00001",
                    IdTrasmittente = new FaPA.Core.FaPa.IdFiscaleType {IdCodice = "01234567890", IdPaese = "IT"}
                }
            };
            
            var anag = new FaPA.Core.FaPa.AnagraficaType
            {
                Denominazione = "AAAA",
                //Cognome = "AAAA",
                //Nome = "AAAA"
            };
            var sede = new FaPA.Core.FaPa.IndirizzoType()
            {
                Indirizzo = "ddd",
                NumeroCivico = "d1l1",
                Provincia = "CZ",
                Comune="comune",
                CAP = "88051",
                Nazione = "IT"
            };

            fatturaElettronicaTypeV11.FatturaElettronicaHeader.CedentePrestatore =
                new FaPA.Core.FaPa.CedentePrestatoreType
                {
                    DatiAnagrafici = new FaPA.Core.FaPa.DatiAnagraficiCedenteType
                    {
                        Anagrafica = anag,
                        IdFiscaleIVA = new FaPA.Core.FaPa.IdFiscaleType()
                        {
                            IdCodice = "01234567890", IdPaese = "IT"
                        },
                        RegimeFiscale = FaPA.Core.FaPa.RegimeFiscaleType.RF01
                    },
                    Sede = sede
                };

            fatturaElettronicaTypeV11.FatturaElettronicaHeader.CessionarioCommittente = 
                new FaPA.Core.FaPa.CessionarioCommittenteType()
                {
                    DatiAnagrafici = new FaPA.Core.FaPa.DatiAnagraficiCessionarioType()
                    {
                        Anagrafica = anag, CodiceFiscale = "09876543210"
                    },
                    Sede = sede
                };
            Decimal importo = (decimal) 14.24;
            fatturaElettronicaTypeV11.FatturaElettronicaBody = new FaPA.Core.FaPa.FatturaElettronicaBodyType()
            {
                DatiGenerali = new FaPA.Core.FaPa.DatiGeneraliType()
                {
                    DatiGeneraliDocumento = new FaPA.Core.FaPa.DatiGeneraliDocumentoType()
                    {
                        Divisa = "EUR",
                        Numero = "01",
                        //Causale = new string[] { "causale" } ,
                        Data = DateTime.Now,
                        ImportoTotaleDocumento = importo
                    }
                }
                
            };

            var ouPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) + @"\file.xml";
            SerializationHelpers.SerializeToDisk( ouPath, fatturaElettronicaTypeV11 );

            //var xmlData = FatturaPaType.XmlSerializeToString(fatturaElettronicaTypeV11);
            //var document = XDocument.Parse(xmlData);
            //TestHelpers.ValidateXmlFattura(document, _schema);
            //var f = FatturaPaType.XmlDeserializeFromString( xmlData );

            TestHelpers.ValidateXmlFatturaFromDisk(ouPath, _schema);
        }

        [Test]
        public void ValidateXmlDiskStream()
        {
            var ouPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\file.xml";
            TestHelpers.ValidateXmlFatturaFromDisk( ouPath, _schema );
        }
    }
}