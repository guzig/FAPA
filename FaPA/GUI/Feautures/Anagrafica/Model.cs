using System.Collections;
using System.Collections.ObjectModel;
using FaPA.GUI.Controls.MyTabControl;
using NHibernate;

namespace FaPA.GUI.Feautures.Anagrafica
{
    public class Model: BaseCrudModel
    {
        private EditAnagraficaViewModel _editViewModel;
        //ctor
        public override void SetEditViewModel(ISession session, IBasePresenter basePresenter)
        {
            _editViewModel = new EditAnagraficaViewModel(basePresenter, UserEntities, UserCollectionView, session);
            _editViewModel.DisplayName = "Dettaglio anagrafica";

            //_editViewModel.AddCrossCoupledPropValidation((CentroDiCosto c) => c.Funzione)
            //              .AddCrossCoupledPropValidation((CentroDiCosto c) => c.Servizio)
            //              .AddCrossCoupledPropValidation((CentroDiCosto c) => c.Intervento)
            //              .AddCrossCoupledPropValidation((CentroDiCosto c) => c.CapitoloSpesa);

            //when NH require to call ValidateInstance to validate single property 
            //(e.g. when the constraint involves more than one property
            //even if this is possible to baypass this with a pathc to NHV)
            //each validation maybe affect also the properties that go to db for validation
            //even if we use NH 2 level cache we have minor number of query during cache creation
            //...so the trick we use is an in-memory cache to simulate property-level for binding UI purpose validation 
            //and to reduce round-trips to Db
            //_editViewModel.AddEntityLevelPropValidation((CentroDiCosto c) => c.Funzione)
            //              .AddEntityLevelPropValidation((CentroDiCosto c) => c.Servizio)
            //              .AddEntityLevelPropValidation((CentroDiCosto c) => c.Intervento)
            //              .AddEntityLevelPropValidation((CentroDiCosto c) => c.CapitoloSpesa);

            var listViewViewModel = new AnagraficaListViewModel
            {
                DisplayName = DisplayName,
                UserEntitiesView = UserCollectionView
            };

            Workspaces.Add(listViewViewModel);

            if ( UserEntities != null && UserEntities.Count > 0)
            {
                Workspaces.Add( _editViewModel );
            }

        }

        private ObservableCollection<Core.Anagrafica> _userEntities;

        public override IList UserEntities
        {
            get { return _userEntities; }
            set
            {
                _userEntities = (ObservableCollection<Core.Anagrafica>)value;
                NotifyOfPropertyChange(() => UserEntities);
            }
        }

        public override WorkspaceViewModel EditViewModel
        {
            get { return _editViewModel; }
        }

        public override string DisplayName => "Anagrafica";
    }
}
