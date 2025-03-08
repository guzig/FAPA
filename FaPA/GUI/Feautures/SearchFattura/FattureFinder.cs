using System;
using FaPA.Infrastructure.Finder;
using NHibernate.Criterion;

namespace FaPA.GUI.Feautures.SearchFattura
{
    public class FattureFinder : ObjectFinder
    {
        public IQueryCriteria FatturaId { get; set; }

        public IQueryCriteria NumFattura { get; set; }

        public IQueryCriteria DataFattura { get; set; }

        public IQueryCriteria DataCaricamento { get; set; }

        public IQueryCriteria Fornitore { get; set; }

        public IQueryCriteria Committente { get; set; }

        public IQueryCriteria TotaleFattura { get; set; }

        public IQueryCriteria CigDB { get; set; }

        public IQueryCriteria CupDB { get; set; }

        public IQueryCriteria CodUfficioDB { get; set; }

        public IQueryCriteria RiferimentoAmmDB { get; set; }

        public IQueryCriteria ProgFile { get; set; }

        public IQueryCriteria FormatoTrasmissioneDB { get; set; }

        public IQueryCriteria PecDestinatarioDB { get; set; }


        public FattureFinder(Action<string> callBackOnCriteria)
            : base(typeof(WAFEL.Core.CORE.Fattura), callBackOnCriteria)
        {

            QueryCriteria = QueryOver.Of<WAFEL.Core.CORE.Fattura>().Fetch( f => f.AnagraficaCedenteDB ).Eager.
                Fetch( f => f.AnagraficaCommittenteDB ).Eager.
                OrderBy( f => f.DataFatturaDB ).Asc;
            
            SetUpFinderProps();
        }

        private void SetUpFinderProps()
        {
            FatturaId = CreateSearchProperty( (WAFEL.Core.CORE.Fattura f ) => f.Id, "Id" );

            NumFattura = CreateSearchProperty( (WAFEL.Core.CORE.Fattura f ) => f.NumeroFatturaDB, "Numero" );

            DataFattura = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.DataFatturaDB, "Data" );

            DataCaricamento = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.DataCaricamentoDB, "Data caricamento" );

            Committente = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.AnagraficaCommittenteDB, typeof( CommittenteSearchProperty ),
                ( WAFEL.Core.CORE.Fattura f ) => f.AnagraficaCommittenteDB.Denom, "Committente" );

            Fornitore = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.AnagraficaCedenteDB, typeof( FornitoreSearchProperty ),
                ( WAFEL.Core.CORE.Fattura f ) => f.AnagraficaCedenteDB.Denom, "Fornitore" );

            TotaleFattura = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.TotaleFatturaDB, "Totale fattura" );

            CigDB = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.CigDB, "CIG" );
            CupDB = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.CupDB, "CUP" );
            CodUfficioDB = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.CodUfficioDB, "Cod. Ufficio" );
            PecDestinatarioDB = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.PecDestinatarioDB, "PEC destin." );
            ProgFile = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.ProgFile, "Progr. invio" );
            FormatoTrasmissioneDB = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.FormatoTrasmissioneDB, "Formato" );
            RiferimentoAmmDB = CreateSearchProperty( ( WAFEL.Core.CORE.Fattura f ) => f.RiferimentoAmmDB, "Rif. Amm." );

            //StatoFattura = CreateSearchProperty( ( Core.Fattura f ) => f.StatoFattura, "Stato fattura" );
        }

    }
}