using System;
using FaPA.Infrastructure.Finder;
using NHibernate.SqlCommand;

namespace FaPA.GUI.Feautures.SearchFattura
{
    public class DettaglioFatturaFinder : CollectionFinder
    {
        public DettaglioFatturaFinder( Type rootType, JoinType joinType, Action<string> callBackOnCriteria, string propName ) :
            base( rootType, joinType, callBackOnCriteria, propName )
        {

            //Id = CreateSearchProperty( ( DettaglioFattura f ) => f.Id, "Id" );

            //Descrizione = CreateSearchProperty( ( DettaglioFattura f ) => f.Descrizione, "Descrizione" );

            //Quantità = CreateSearchProperty( ( DettaglioFattura f ) => f.Quantità, "Quantità" );

            //Prezzo = CreateSearchProperty( ( DettaglioFattura f ) => f.Prezzo, "Prezzo" );

            //Importo = CreateSearchProperty( ( DettaglioFattura f ) => f.Importo, "Importo" );

            //CausaleIva = CreateSearchProperty( ( DettaglioFattura f ) => f.CausaleIva, "Causale IVA" );

            //Periodo = CreateSearchProperty( ( Core.Fattura f ) => f.PeriodoFatturazione, typeof(PeriodoSearchProperty),
            //    ( Core.Fattura f ) => f.PeriodoFatturazione.Periodo, "Periodo" );

            //DataInizioPeriodo = CreateSearchProperty( ( Core.Fattura f ) => f.PeriodoFatturazione.InizioPeriodo, "Inizio periodo" );

            //DataFinePeriodo = CreateSearchProperty( ( Core.Fattura f ) => f.PeriodoFatturazione.FinePeriodo, "Fine periodo" );
   
        }

        public IQueryCriteria Id { get; set; }
        public IQueryCriteria Descrizione { get; set; }
        public IQueryCriteria Quantità { get; set; }
        public IQueryCriteria Prezzo { get; set; }
        public IQueryCriteria Importo { get; set; }
        public IQueryCriteria CausaleIva { get; set; }
        public IQueryCriteria Periodo { get; set; }
        public IQueryCriteria DataInizioPeriodo { get; set; }
        public IQueryCriteria DataFinePeriodo { get; set; }
        //public PodFinder PoDFinder { get; set; }
        //public IQueryCriteria CausaleDettaglio { get; set; }
    }
}