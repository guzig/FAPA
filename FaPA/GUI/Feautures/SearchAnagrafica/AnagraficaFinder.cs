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

        //public ImpegniFinanziariFinder ImpegniFinanziariFinder { get; set; }

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
            Nome = CreateSearchProperty( (Core.Anagrafica f) => f.Nome, "Nome");
            CodiceFiscale = CreateSearchProperty( (Core.Anagrafica f) => f.CodiceFiscale, "CodiceFiscale");


            //ImpegniFinanziariFinder = new ImpegniFinanziariFinder( typeof( ImpegnoFinanziario ), JoinType.None,
            //    null, "Anagrafica" );

            //Associations.Add( "Impegni", ImpegniFinanziariFinder );

            //--

            //CapitoloSpesa = CreateSearchProperty(f => f.CapitoloSpesa, typeof(CapitoloSearchProperty),
            //    (CentroDiCosto f) => f.CapitoloSpesa.Descrizione, "CapitoloSpesa");

            //SetFetchingStrategies = detaCrit => detaCrit.SetFetchMode("CapitoloSpesa", FetchMode.Join);
        }

    }
}
