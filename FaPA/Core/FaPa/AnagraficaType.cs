using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
    public class AnagraficaType : BaseEntityFpa
    {
        private string _titoloField;
        private string _codEoriField;
        private string _cognome;
        private string _nome;
        private string _denominazione;

        public virtual string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        public virtual string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public virtual string Denominazione
        {
            get { return _denominazione; }
            set { _denominazione = value; }
        }

        public virtual string Titolo
        {
            get
            {
                return _titoloField;
            }
            set
            {
                _titoloField = value;
            }
        }

        public virtual string CodEORI
        {
            get
            {
                return _codEoriField;
            }
            set
            {
                _codEoriField = value;
            }
        }
    }
}