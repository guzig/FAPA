namespace FaPA.Core
{
    public class Comune : BaseEntity
    {
        public virtual string CodiceRegione { get; set; }
        public virtual string CodiceCitt‡Metropolitana { get; set; }
        public virtual string CodiceProvincia { get; set; }
        public virtual string CodiceAlpha { get; set; }
        public virtual string Denominazione { get; set; }
        public virtual string NomeRegione { get; set; }
        public virtual string NomeCitt‡Metropolitana { get; set; }
        public virtual string DenominazioneProvincia { get; set; }
        public virtual string FlagComuneCapoluogo { get; set; }
        public virtual string SiglaAuto { get; set; }
        public virtual string CodiceCatastale { get; set; }
        public virtual string CodiceComune { get; set; }
        public virtual string Cap { get; set; }

        public override DomainResult Validate()
        {
            DomainResult = new DomainResult(true);
            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            return Validate();
        }

    }
}