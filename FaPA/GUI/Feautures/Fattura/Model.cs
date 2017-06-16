using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure.Helpers;
using NHibernate;

namespace FaPA.GUI.Feautures.Fattura
{
    public class Model : ModelBase
	{
        EditFatturaViewModel _editViewModel;
        public override WorkspaceViewModel EditViewModel
        {
            get { return _editViewModel; }
        }

        public override void SetEditViewModel(ISession session, IBasePresenter baseCrudPresenter)
        {
            _editViewModel = new EditFatturaViewModel(baseCrudPresenter, UserEntities, UserCollectionView, session);
            _editViewModel.DisplayName = "Fattura";


            //when NH require to call ValidateInstance to validate single property 
            //(e.g. when the constraint involves more than one property even if is possible to baypass this with a path to NHV)
            //each validation maybe affect also the properties that go to db for validation
            //even if we use NH 2 level cache we have minor number of query during cache loading
            //...so the trick we use is an in-memory cache to simulate property-level for binding UI purpose validation 
            //and to reduce round-trips to Db

            _editViewModel.AddEntityLevelPropValidation( ( Core.Fattura f ) => f.DataFatturaDB )
                .AddEntityLevelPropValidation((Core.Fattura f) => f.TotaleFatturaDB)
                .AddEntityLevelPropValidation((Core.Fattura f) => f.NumeroFatturaDB)
                .AddEntityLevelPropValidation((Core.Fattura f) => f.AnagraficaCedenteDB);

            if (Workspaces != null && !Workspaces.Any( w => w is FatturaListViewModel ) )
            {
                //we want fisrt show user filtered collection to show end eventually process for CRUD
                var listViewViewModel = new FatturaListViewModel
                {
                    DisplayName = DisplayName,
                    UserEntitiesView = UserCollectionView
                };

                Workspaces.Add(listViewViewModel);               
            }

            //after the mandatory workspace for editing purpose
            if ( UserEntities != null && UserEntities.Count > 0)
            {
                Workspaces.Add(_editViewModel);
            }
        }
        
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