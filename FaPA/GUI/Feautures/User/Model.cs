using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;
using FaPA.Core;
using FaPA.GUI.Controls.MyTabControl;
using NHibernate;

namespace FaPA.GUI.Feautures.User
{
    class Model : BaseCrudModel
    {
        EditUserViewModel _editViewModel;

        public override WorkspaceViewModel EditViewModel
        {
            get { return _editViewModel; }
        }


        public override void SetEditViewModel(ISession session, IBasePresenter basePresenter)
        {
            //_editViewModel = new EditUserViewModel(basePresenter, UserEntities, UserCollectionView, session);
            //_editViewModel.DisplayName = "Dettaglio utente";
            
            //_editViewModel.AddCrossCoupledPropValidation((AggregatoDiCosto c) => c.MetaCategoria)
            //              .AddCrossCoupledPropValidation((AggregatoDiCosto c) => c.Descrizione);

            //when NH require to call ValidateInstance to validate single property 
            //(e.g. when the constraint involves more than one property
            //even if this is possible to baypass this with a path to NHV)
            //each validation maybe affect also the properties that go to db for validation
            //even if we use NH 2 level cache we have minor number of query during cache creation
            //...so the trick we use is an in memory cache to simulate property-level for binding UI purpose validation 
            //and to reduce round-trips to Db
            
			//_editViewModel.AddEntityLevelPropValidation((AggregatoDiCosto c) => c.MetaCategoria)
            //              .AddEntityLevelPropValidation((AggregatoDiCosto c) => c.Descrizione);
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
