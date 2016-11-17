using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiBeniServiziType : BaseEntityFpa
    {
        private DettaglioLineeType[] _dettaglioLineeField;
        private DatiRiepilogoType[] _datiRiepilogoField;

        [XmlElement]
        public virtual DettaglioLineeType[] DettaglioLinee
        {
            get
            {               
                return _dettaglioLineeField;
            }
            set
            {
                _dettaglioLineeField = value;
            }
        }

        [XmlElement]
        public virtual DatiRiepilogoType[] DatiRiepilogo
        {
            get
            {
                return _datiRiepilogoField;
            }
            set
            {
                _datiRiepilogoField = value;
            }
        }
    }
}