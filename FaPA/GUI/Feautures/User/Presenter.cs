using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using FaPA.Core;
using FaPA.DomainServices.Utils;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Design.Events;
using FaPA.GUI.Feautures.LogIn;
using FaPA.GUI.Utils;
using FaPA.Infrastructure.Helpers;
using NHibernate.Criterion;

namespace FaPA.GUI.Feautures.User
{    
    public class Presenter : BaseCrudPresenter<UserData, View>
    {
        public Presenter()
        {
            View.Presenter = this;
        }

        public void Initialize(Action<Presenter> onLoaded)
        {
            _onLoaded = onLoaded;
        }
        
        public void OnLoaded()
        {
            _onLoaded(this);
        }

        private string _onGridEmptyText = "Inserisci un nuovo utente";
        public string OnGridEmptyText
        {
            get { return _onGridEmptyText; }
            set { _onGridEmptyText = value; }
        }
        
        private Action<Presenter> _onLoaded;
        
        //private FinderConfirmSearchEventArgs _dataFiltered;

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                if (_search != null) return _search;
                _search = new RelayCommand(param => { }, param => false);
                return _search;
            }

        }
        
        //private void OnSearch()
        //{
        //    var presenter = Presenters.CreateInstance("SearchAggregatiDiCosto", null) as SearchAggregatiDiCosto.Presenter;
        //    presenter.ConfirmResult += OnConfirmResult;
        //    presenter.ShowDialog();
        //    if (presenter == null)
        //        throw new Exception();
        //}

        private ICommand _entityChoosenCommand;
        public ICommand EntityChoosenCommand
        {
            get
            {
                if ( _entityChoosenCommand != null )
                    return _entityChoosenCommand;
                _entityChoosenCommand = new RelayCommand( e => SetActiveWorkspace(
                    Model.Workspaces.First( w => w is EditUserViewModel ) ), o => true );
                return _entityChoosenCommand;
            }
        }

        //private void OnConfirmResult(object sender, FinderConfirmSearchEventArgs data)
        //{
        //    _dataFiltered = data;
        //    if (_dataFiltered.DetachedCriteria != null)
        //        CreateNewModel(_dataFiltered.DetachedCriteria);
        //    else
        //    {
        //        if (_dataFiltered.Collection.Count > 0)
        //            CreateNewModel(_dataFiltered.Collection);
        //    }
        //}

        public override void CreateNewModel(DetachedCriteria queryByExample)
        {
            var entities = GetExeCriteria<UserData>(queryByExample);
            SetUpModel(entities);
        }

        private void CreateNewModel( IList searchResult )
        {
            var entitiesdto = new ObservableCollection<UserData>(
                searchResult.Cast<UserData>().ToList() );

            if ( IsIncrementalSearch() )
            {
                foreach ( var userEntity in Model.UserEntities.Cast<UserData>()
                    .Where( userEntity => !entitiesdto.Contains( userEntity ) ) )
                {
                    entitiesdto.Add( userEntity );
                }

            }
            Model.UserEntities = entitiesdto;
            Model.UserCollectionView = CollectionViewSource.GetDefaultView( Model.UserEntities );

        }

        public override void CreateNewModel(int activeTab)
        {
            IsBusy = true;
            var entities = GetExeCriteria<Core.UserData>( QueryCriteria );
            var entitiesdto = entities == null ? new ObservableCollection<Core.UserData>()
                : new ObservableCollection<Core.UserData>( entities );
            SetUpNewModel( 0, entitiesdto );
            IsBusy = false;
        }

        public override void CreateNewModel(QueryOver queryByExample)
        {
            IsBusy = true;
            
            Model = new Model();
            
            //InitViewModel<UserData>( queryByExample );
            
            IsBusy = false;
        }

        protected override QueryOver QueryCriteria { get; set; } = QueryOver.Of<UserData>().
            Where( u => u.UserName != "em" );
        
        protected override BaseCrudModel CreateNewModel()
        {
            return new Model();
        }

        #region Entity lifecylce Events

        protected override void RegisterEntityAddedNewEvent()
        {
            //EventPublisher.Register<UserAddedNew>( RefreshAfterAddedNew );
        }

        #endregion

        #region INotifyDataSourceHit Members

        public override void QueryInProgress( bool inProgress )
        {
            //Model.EditViewModel. = inProgress;
        }

        #endregion

        private ICommand _restePasswordCommand;
        public ICommand ResetPasswordCommand
        {
            get
            {
                if ( _restePasswordCommand != null )
                    return _restePasswordCommand;
                _restePasswordCommand = new RelayCommand( e => ResetPassword(), o => CanResetPassword() );
                return _restePasswordCommand;
            }
        }

        private bool CanResetPassword()
        {
            var currentEntity = ( ( EditUserViewModel ) Model.EditViewModel ).CurrentEntity;

            return currentEntity != null  && currentEntity.Id>0;
        }

        private void ResetPassword()
        {
            var currentEntity = ( ( EditUserViewModel ) Model.EditViewModel ).CurrentEntity;

            currentEntity.ResetPassword();

            try
            {
                using (NHhelper.Instance.OpenUnitOfWork())
                {
                    using (var tx = NHhelper.Instance.CurrentSession.BeginTransaction())
                    {
                        NHhelper.Instance.CurrentSession.Update(currentEntity);
                        tx.Commit();
                    }
                }

                AuthenticationServiceLocator.Service.AuthenticateUser(currentEntity.UserName, null);
            }
            catch (Exception e)
            {
                const string msg = "L'operazione di reset della password è fallita per un errore imprevisto!";
                const string caption = "La password non è stata resettata";
                Xceed.Wpf.Toolkit.MessageBox.Show( msg, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation );
            }
        }
    }
}
