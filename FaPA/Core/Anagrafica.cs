using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FaPA.Core
{
    public abstract class Anagrafica : BaseEntity
    {
        public virtual string Cognome { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Denominazione { get; set; }
        public virtual string CodiceFiscale { get; set; }
        public virtual string PIva { get; set; }
        public virtual string Indirizzo { get; set; }
        public virtual string Civico { get; set; }
        public virtual string Cap { get; set; }
        public virtual string Comune { get; set; }
        public virtual string Provincia { get; set; }
        public virtual string Nazione { get; set; }
        public virtual string CodUfficio { get; set; }
        public virtual string Pec { get; set; }
        public virtual string Email { get; set; }
        public virtual string Tel { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Note { get; set; }

        public virtual string Denom
        {
            get { return !string.IsNullOrWhiteSpace( Denominazione ) ? Denominazione : Cognome + " " + Nome; }
        }

        public virtual ICollection<Fattura> Fatture { get; } = new Collection<Fattura>();
        
        public override DomainResult Validate()
        {
            var errors = new Dictionary<string, IEnumerable<string>>();

            if ( Email.IsValidEmail() )
                errors.Add("Email", new[] { "Email non valida" } );

            GetPersistentErrors(errors);

            DomainResult = new DomainResult(errors);

            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            Validate();
            return DomainResult;
        }
    }
}