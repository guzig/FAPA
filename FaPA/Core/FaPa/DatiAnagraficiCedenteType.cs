using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class DatiAnagraficiCedenteType: BaseEntityFpa
    {
        private IdFiscaleType idFiscaleIVAField;
        private string codiceFiscaleField;
        private AnagraficaType anagraficaField;
        private string alboProfessionaleField;
        private string provinciaAlboField;
        private string numeroIscrizioneAlboField;
        private DateTime dataIscrizioneAlboField;
        private bool dataIscrizioneAlboFieldSpecified;
        private RegimeFiscaleType regimeFiscaleField;

        public virtual IdFiscaleType IdFiscaleIVA
        {
            get
            {
                return idFiscaleIVAField;
            }
            set
            {
                idFiscaleIVAField = value;
            }
        }

        public virtual string CodiceFiscale
        {
            get
            {
                return codiceFiscaleField;
            }
            set
            {
                codiceFiscaleField = value;
            }
        }

        public virtual AnagraficaType Anagrafica
        {
            get
            {
                return anagraficaField;
            }
            set
            {
                anagraficaField = value;
            }
        }

        public virtual string AlboProfessionale
        {
            get
            {
                return alboProfessionaleField;
            }
            set
            {
                alboProfessionaleField = value;
            }
        }

        public virtual string ProvinciaAlbo
        {
            get
            {
                return provinciaAlboField;
            }
            set
            {
                provinciaAlboField = value;
            }
        }

        public virtual string NumeroIscrizioneAlbo
        {
            get
            {
                return numeroIscrizioneAlboField;
            }
            set
            {
                numeroIscrizioneAlboField = value;
            }
        }

        public virtual DateTime DataIscrizioneAlbo
        {
            get
            {
                return dataIscrizioneAlboField;
            }
            set
            {
                dataIscrizioneAlboField = value;
            }
        }

        [XmlIgnore]
        public virtual bool DataIscrizioneAlboSpecified
        {
            get
            {
                return dataIscrizioneAlboFieldSpecified;
            }
            set
            {
                dataIscrizioneAlboFieldSpecified = value;
            }
        }

        public virtual RegimeFiscaleType RegimeFiscale
        {
            get
            {
                return regimeFiscaleField;
            }
            set
            {
                regimeFiscaleField = value;
            }
        }
    }
}