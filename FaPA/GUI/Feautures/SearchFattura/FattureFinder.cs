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


        //public PodFinder PoDFinder { get; set; }
        
        //public LiquidazioneFinder LiquidazioneFinder { get; set; }

        //public DettaglioFatturaFinder DettaglioFatturaFinder { get; set; }

        public FattureFinder(Type rootType, Action<string> callBackOnCriteria)
            : base(rootType, callBackOnCriteria)
        {
            ////PoDFinder = new PodFinder(typeof(PoD), "CodicePoD", JoinType.InnerJoin, callBackOnCriteria);

            QueryCriteria = QueryOver.Of<Core.Fattura>().Fetch( f => f.AnagraficaCedenteDB ).Eager.
                Fetch( f => f.AnagraficaCommittenteDB ).Eager.
                    OrderBy( f => f.DataFatturaDB ).Asc;
            
            SetUpFinderProps();
        }

        private void SetUpFinderProps()
        {
            FatturaId = CreateSearchProperty( ( Core.Fattura f ) => f.Id, "Id" );

            NumFattura = CreateSearchProperty( ( Core.Fattura f ) => f.NumeroFatturaDB, "Numero" );

            DataFattura = CreateSearchProperty( ( Core.Fattura f ) => f.DataFatturaDB, "Data" );

            //DataCaricamento = CreateSearchProperty( ( Core.Fattura f ) => f.DataCaricamento, "Data caricamento" );

            Committente = CreateSearchProperty( ( Core.Fattura f ) => f.AnagraficaCommittenteDB, typeof( CommittenteSearchProperty ),
                ( Core.Fattura f ) => f.AnagraficaCommittenteDB.Denom, "Committente" );

            Fornitore = CreateSearchProperty( ( Core.Fattura f ) => f.AnagraficaCedenteDB, typeof( FornitoreSearchProperty ),
                ( Core.Fattura f ) => f.AnagraficaCedenteDB.Denom, "Fornitore" );

            TotaleFattura = CreateSearchProperty( ( Core.Fattura f ) => f.TotaleFatturaDB, "Totale fattura" );

            //StatoFattura = CreateSearchProperty( ( Core.Fattura f ) => f.StatoFattura, "Stato fattura" );
        }

    }
}