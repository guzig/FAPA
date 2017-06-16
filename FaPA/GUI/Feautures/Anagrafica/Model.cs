using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using FaPA.GUI.Controls.MyTabControl;
using NHibernate;

namespace FaPA.GUI.Feautures.Anagrafica
{
    public class Model: ModelBase
    {
        private EditAnagraficaViewModel _editViewModel;
        
        //ctor
        public override void SetEditViewModel(ISession session, IBasePresenter basePresenter)
        {
            _editViewModel = new EditAnagraficaViewModel(basePresenter, UserEntities, UserCollectionView, session);
            _editViewModel.DisplayName = "Dettaglio anagrafica";

            //we want fisrt show user filtered collection to show end eventually process for CRUD
            if ( Workspaces != null && !Workspaces.Any( w => w is AnagraficaListViewModel ) ) 
            {
                Workspaces.Add( new AnagraficaListViewModel
                {
                    DisplayName = DisplayName,
                    UserEntitiesView = UserCollectionView
                } );
            }

            //after the mandatory workspace for editing purpose
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
