using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiGeneraliType : BaseEntityFpa
    {
        private DatiGeneraliDocumentoType _datiGeneraliDocumentoField;
        private DatiDocumentiCorrelatiType[] _datiOrdineAcquistoField;
        private DatiDocumentiCorrelatiType[] _datiContrattoField;
        private DatiDocumentiCorrelatiType[] _datiConvenzioneField;
        private DatiDocumentiCorrelatiType[] _datiRicezioneField;
        private DatiDocumentiCorrelatiType[] _datiFattureCollegateField;
        private DatiSALType[] _datiSalField;
        private DatiDDTType[] _datiDdtField;
        private DatiTrasportoType _datiTrasportoField;
        private FatturaPrincipaleType _fatturaPrincipaleField;
        
        public virtual DatiGeneraliDocumentoType DatiGeneraliDocumento
        {
            get
            {
                return _datiGeneraliDocumentoField;
            }
            set
            {
                _datiGeneraliDocumentoField = value;
            }
        }

        [XmlElement( "DatiOrdineAcquisto", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiDocumentiCorrelatiType[] DatiOrdineAcquisto
        {
            get
            {
                return _datiOrdineAcquistoField;
            }
            set
            {
                _datiOrdineAcquistoField = value;
            }
        }
        
        [XmlElement( "DatiContratto", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiDocumentiCorrelatiType[] DatiContratto
        {
            get
            {
                return _datiContrattoField;
            }
            set
            {
                _datiContrattoField = value;
            }
        }
        
        [XmlElement( "DatiConvenzione", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiDocumentiCorrelatiType[] DatiConvenzione
        {
            get
            {
                return _datiConvenzioneField;
            }
            set
            {
                _datiConvenzioneField = value;
            }
        }
        
        [XmlElement( "DatiRicezione", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiDocumentiCorrelatiType[] DatiRicezione
        {
            get
            {
                return _datiRicezioneField;
            }
            set
            {
                _datiRicezioneField = value;
            }
        }
        
        [XmlElement( "DatiFattureCollegate", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiDocumentiCorrelatiType[] DatiFattureCollegate
        {
            get
            {
                return _datiFattureCollegateField;
            }
            set
            {
                _datiFattureCollegateField = value;
            }
        }
        
        [XmlElement( "DatiSAL", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiSALType[] DatiSAL
        {
            get
            {
                return _datiSalField;
            }
            set
            {
                _datiSalField = value;
            }
        }
        
        [XmlElement( "DatiDDT", Form = XmlSchemaForm.Unqualified )]
        public virtual DatiDDTType[] DatiDDT
        {
            get
            {
                return _datiDdtField;
            }
            set
            {
                _datiDdtField = value;
            }
        }
        
        public virtual DatiTrasportoType DatiTrasporto
        {
            get
            {
                return _datiTrasportoField;
            }
            set
            {
                _datiTrasportoField = value;
            }
        }
        
        public virtual FatturaPrincipaleType FatturaPrincipale
        {
            get
            {
                return _fatturaPrincipaleField;
            }
            set
            {
                _fatturaPrincipaleField = value;
            }
        }
    }
}