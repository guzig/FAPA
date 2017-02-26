using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiAnagraficiTerzoIntermediarioType : BaseEntityFpa
    {
        private IdFiscaleType _idFiscaleIvaField;
        private string _codiceFiscaleField;
        private AnagraficaType _anagraficaField;
        private bool _idFiscaleIvaSpecified;
        private bool _codiceFiscaleSpecified;

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual IdFiscaleType IdFiscaleIVA
        {
            get
            {
                return _idFiscaleIvaField;
            }
            set
            {
                _idFiscaleIvaField = value;
                IdFiscaleIVASpecified = IdFiscaleIVA != null && ( !string.IsNullOrWhiteSpace(IdFiscaleIVA.IdCodice ) || 
                    !string.IsNullOrWhiteSpace( IdFiscaleIVA.IdPaese ) );
            }
        }

        [XmlIgnore]
        public bool IdFiscaleIVASpecified
        {
            get { return _idFiscaleIvaSpecified; }
            set { _idFiscaleIvaSpecified = value; }
        }

        public virtual string CodiceFiscale
        {
            get
            {
                return _codiceFiscaleField;
            }
            set
            {
                _codiceFiscaleField = value;
                CodiceFiscaleSpecified = !string.IsNullOrWhiteSpace( _codiceFiscaleField );
            }
        }

        [XmlIgnore]
        public bool CodiceFiscaleSpecified
        {
            get { return _codiceFiscaleSpecified; }
            set { _codiceFiscaleSpecified = value; }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual AnagraficaType Anagrafica
        {
            get
            {
                return _anagraficaField;
            }
            set
            {
                _anagraficaField = value;
            }
        }
    }
}