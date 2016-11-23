using System.Collections;
using System.Collections.ObjectModel;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure.Helpers;
using NHibernate;

namespace FaPA.GUI.Feautures.Fattura
{
    
    public class Model : BaseCrudModel
	{
        EditFatturaViewModel _editViewModel;
        public override WorkspaceViewModel EditViewModel
        {
            get { return _editViewModel; }
        }

        public override void SetEditViewModel(ISession session, IBasePresenter baseCrudPresenter)
        {
            _editViewModel = new EditFatturaViewModel(baseCrudPresenter, UserEntities, UserCollectionView, session);

            //_editViewModel.SetUpCollectionView(UserEntities, UserCollectionView);
            _editViewModel.DisplayName = "Fattura";
            //_editViewModel.AddCrossCoupledPropValidation((FatturaDto f) => f.DataFatturaDB)
            //                .AddCrossCoupledPropValidation((FatturaDto f) => f.Liquidazione.DataLiquidazione);

            //when NH require to call ValidateInstance to validate single property 
            //(e.g. when the constraint involves more than one property
            //even if this is possible to baypass this with a path to NHV)
            //each validation maybe affect also the properties that go to db for validation
            //even if we use NH 2 level cache we have minor number of query during cache creation
            //...so the trick we use is an in-memory cache to simulate property-level for binding UI purpose validation 
            //and to reduce round-trips to Db

            _editViewModel.AddEntityLevelPropValidation( ( Core.Fattura f ) => f.DataFatturaDB )
                .AddEntityLevelPropValidation((Core.Fattura f) => f.TotaleFatturaDB)
                .AddEntityLevelPropValidation((Core.Fattura f) => f.NumeroFatturaDB)
                .AddEntityLevelPropValidation((Core.Fattura f) => f.AnagraficaCedenteDB);

            var listViewViewModel = new FatturaListViewModel
            {
                DisplayName = DisplayName,
                UserEntitiesView = UserCollectionView
            };

            Workspaces.Add(listViewViewModel);

            if (UserEntities != null && UserEntities.Count > 0)
            {
                Workspaces.Add(_editViewModel);
            }
        }
        
        //public ICollectionView Fatture { get; set; }

		public GenericsObservable<bool> AllowEditing { get; set; }

        private ObservableCollection<Core.Fattura> _userEntities;
        public override IList UserEntities
        {
            get { return _userEntities; }
            set
            {
                _userEntities = ( ObservableCollection<Core.Fattura> )value;
                NotifyOfPropertyChange(() => UserEntities);
            }
        }

        public override string DisplayName
        {
            get { return "Fatture"; }
        }

	}
}