using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class RappresentanteCessionarioType: BaseEntityFpa
    {
        private IdFiscaleType _idFiscaleIvaField;
        private string _denominazioneField;
        private string _nomeField;
        private string _cognomeField;

        public IdFiscaleType IdFiscaleIVA
        {
            get { return _idFiscaleIvaField; }
            set { _idFiscaleIvaField = value; }
        }

        public string Denominazione
        {
            get { return _denominazioneField; }
            set { _denominazioneField = value; }
        }

        public string Nome
        {
            get { return _nomeField; }
            set { _nomeField = value; }
        }

        public string Cognome
        {
            get { return _cognomeField; }
            set { _cognomeField = value; }
        }
    }
}