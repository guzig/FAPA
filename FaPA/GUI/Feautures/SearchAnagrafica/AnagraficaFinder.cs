using System;
using FaPA.Infrastructure.Finder;
using NHibernate.SqlCommand;

namespace FaPA.GUI.Feautures.SearchAnagrafica
{
    public class AnagraficaFinder : ObjectFinder
    {
        public IQueryCriteria AnagraficaId { get; set; }
        public IQueryCriteria Denominazione { get; set; }
        public IQueryCriteria Cognome { get; set; }
        public IQueryCriteria Nome { get; set; }
        public IQueryCriteria CodiceFiscale { get; set; }
        public IQueryCriteria PIva { get; set; }
        public IQueryCriteria Indirizzo { get; set; }
        public IQueryCriteria Civico { get; set; }
        public IQueryCriteria Cap { get; set; }
        public IQueryCriteria Comune { get; set; }
        public IQueryCriteria Provincia { get; set; }
        public IQueryCriteria Nazione { get; set; }
        public IQueryCriteria CodUfficioPa { get; set; }
        public IQueryCriteria Pec { get; set; }
        public IQueryCriteria Email { get; set; }
        public IQueryCriteria Tel { get; set; }
        public IQueryCriteria Fax { get; set; }
        public IQueryCriteria RifAmministrazione { get; set; }

        public AnagraficaFinder(Type rootType, string associationPath, JoinType joinType,
            Action<string> callBackOnCriteria) : base(rootType, joinType,callBackOnCriteria, associationPath)
        {
            Init();
        }

        public AnagraficaFinder( Type rootType ) : base( rootType )
        {
            Init();
        }

        private void Init()
        {
            AnagraficaId = CreateSearchProperty( (Core.Anagrafica f ) => f.Id, "Id");
            Denominazione = CreateSearchProperty( (Core.Anagrafica f) => f.Denominazione, "Rag.Sociale");
            Cognome = CreateSearchProperty( (Core.Anagrafica f) => f.Cognome, "Cognome");
            CodiceFiscale = CreateSearchProperty( ( Core.Anagrafica f ) => f.CodiceFiscale, "CodiceFiscale" );
            PIva = CreateSearchProperty( ( Core.Anagrafica f ) => f.PIva, "P. IVA" );
            Nome = CreateSearchProperty( (Core.Anagrafica f) => f.Nome, "Nome");
            Indirizzo = CreateSearchProperty( ( Core.Anagrafica f ) => f.Indirizzo, "Indirizzo" );
            Civico = CreateSearchProperty( ( Core.Anagrafica f ) => f.Civico, "Civico" );
            Cap = CreateSearchProperty( ( Core.Anagrafica f ) => f.Cap, "Cap" );
            Comune = CreateSearchProperty( ( Core.Anagrafica f ) => f.Comune, "Comune" );
            Provincia = CreateSearchProperty( ( Core.Anagrafica f ) => f.Provincia, "Provincia" );
            Nazione = CreateSearchProperty( ( Core.Anagrafica f ) => f.Nazione, "Nazione" );
            CodUfficioPa = CreateSearchProperty( ( Core.Anagrafica f ) => f.CodUfficioPa, "Cod. Uff. PA" );
            Pec = CreateSearchProperty( ( Core.Anagrafica f ) => f.Pec, "Pec" );
            Email = CreateSearchProperty( ( Core.Anagrafica f ) => f.Email, "Email" );
            Tel = CreateSearchProperty( ( Core.Anagrafica f ) => f.Tel, "Tel" );
            Fax = CreateSearchProperty( ( Core.Anagrafica f ) => f.Fax, "Fax" );
            RifAmministrazione = CreateSearchProperty( ( Core.Anagrafica f ) => f.RifAmministrazione, "Rif. Amm." );

        }

    }
}
