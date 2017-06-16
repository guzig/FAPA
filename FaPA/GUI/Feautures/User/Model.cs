using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using FaPA.Core;
using FaPA.GUI.Controls.MyTabControl;
using NHibernate;

namespace FaPA.GUI.Feautures.User
{
    class Model : ModelBase
    {
        EditUserViewModel _editViewModel;

        public override WorkspaceViewModel EditViewModel
        {
            get { return _editViewModel; }
        }


        public override void SetEditViewModel(ISession session, IBasePresenter basePresenter)
        {
            _editViewModel = new EditUserViewModel( basePresenter, UserEntities, UserCollectionView, session );
            _editViewModel.DisplayName = "Dettaglio utente";

            if ( Workspaces != null && !Workspaces.Any( w => w is UserListViewModel ) )
            {
                Workspaces.Add( new UserListViewModel
                {
                    DisplayName = DisplayName,
                    UserEntitiesView = UserCollectionView
                } );
            }

            if ( UserEntities != null && UserEntities.Count > 0 )
            {
                Workspaces.Add( _editViewModel );
            }

        }

        private ObservableCollection<UserData> _userEntities;

        public override IList UserEntities
        {
            get { return _userEntities; }
            set
            {
                _userEntities = (ObservableCollection<UserData>)value;
                NotifyOfPropertyChange(() => UserEntities);
            }
        }

        public override string DisplayName
        {
            get { return "Utenti"; }
        }

        public static bool IsAdministrator
        {
            get { return Thread.CurrentPrincipal.IsInRole( TipoUtenteEnums.Administrators.ToString() ); }
        }


    }
}
