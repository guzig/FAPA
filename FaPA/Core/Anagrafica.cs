using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FaPA.AppServices;

namespace FaPA.Core
{
    public class Anagrafica : BaseEntity
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
        public virtual string CodUfficioPa { get; set; }
        public virtual string CodSoggettoSDI { get; set; }
        public virtual string Pec { get; set; }
        public virtual string Email { get; set; }
        public virtual string Tel { get; set; }
        public virtual string Fax { get; set; }
        public virtual string RifAmministrazione { get; set; }

        public virtual IList<Comune> ComuniPerProvincia { get; set; }

        public static IList<Comune> Provincie => SharedReferenceDataFactory.Provincie;

        public override PropertyChangedEventHandler PropertyChangedEventHandler => OnPropChanged;

        private void OnPropChanged( object sender, PropertyChangedEventArgs e )
        {
            var proxy = ( Anagrafica ) sender;
            if ( proxy == null )
                return;

            if ( e.PropertyName == nameof( Provincia ) )
            {
                proxy.ComuniPerProvincia = SharedReferenceDataFactory.Comuni.
                    Where( p => p.SiglaProvincia == Provincia ).OrderBy( p => p.Denominazione ).ToList();

                if ( ComuniPerProvincia.Any( c=>c.Denominazione == Comune ) ) return;
                proxy.Comune = null;

                Nazione = "IT";
            }

        }

        public virtual string Denom
        {
            get { return !string.IsNullOrWhiteSpace( Denominazione ) ? Denominazione : Cognome + " " + Nome; }
        }

        public virtual ICollection<Fattura> Fatture { get; protected set; } = new Collection<Fattura>();
        
        public override DomainResult Validate()
        {
            var errors = new Dictionary<string, List<string>>();

            if ( Email.IsValidEmail() )
                errors.Add("Email", new List<string>() { "Email non valida" } );

            GetPersistentErrors(errors);

            DomainResult = new DomainResult(errors);

            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            Validate();
            return DomainResult;
        }

        //public override string ToString()
        //{
        //    return Denom;
        //}
    }
}