using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using FaPA.Core;
using FaPA.DomainServices.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.FlyFetch;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;
using NHibernate.Criterion;

namespace FaPA.GUI.Controls.MyTabControl
{
    public abstract class PresenterBase<T, TView> : AbstractPresenter<ModelBase, TView>,
        IBasePresenter, INotifyDataSourceHit where TView : Window, new() where T : BaseEntity
    {
        //public ListViewViewModel ListViewViewModel { get; private set; }

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get { return Model.Workspaces; }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if ( value.Equals( _isBusy ) ) return;
                _isBusy = value;
                NotifyOfPropertyChange( () => IsBusy );
            }
        }

        private string _allowedGridProperties;
        public string AllowedGridProperties
        {
            get { return _allowedGridProperties; }
            set
            {
                if ( value == _allowedGridProperties ) return;
                _allowedGridProperties = value;
                NotifyOfPropertyChange( () => AllowedGridProperties );
            }
        }

        protected PresenterBase()
        {
            RegisterEntityUpdatedEvent();
            RegisterEntityAddedNewEvent();
            RegisterEntityDeletedEvent();
        }

        protected virtual void RegisterEntityUpdatedEvent() { }
        protected virtual void RegisterEntityAddedNewEvent() { }
        protected virtual void RegisterEntityDeletedEvent() { }

        public WorkspaceViewModel GetActiveWorkSpace()
        {
            return Model.SelectedPage;
        }

        protected abstract QueryOver QueryCriteria { get; set; }

        public abstract void CreateNewModel( QueryOver queryOver );

        protected abstract ModelBase CreateNewModel();

        public abstract void CreateNewModel( int activePage );
        

        //public abstract void CreateNewModel( int activeWorkSpace );

        protected virtual void CreateNewModel<TDto>(int pageSize, ICountProvider pagesProvider, 
            IPageProvider<TDto, ObservableCollection<TDto>> pageProvider, 
            Action<ObservableCollection<TDto>> created) where TDto : new()
        {
            CollectionFactory.Create( pageSize, pageProvider, pagesProvider, created  );
        }

        public abstract void CreateNewModel( DetachedCriteria queryByExample );

        public virtual void RefreshSharedViewsAfterDelete( BaseEntity deletedEntity )
        {
            ReferenceDataFactory.LazyRefresh( typeof( T ) );

            if ( Model.UserEntities.Count != 0 ) return;

            var viewModel = Model.Workspaces.FirstOrDefault( w => w is EditViewModel<T> );

            if ( viewModel != null )
            {
                ( ( EditViewModel<T> ) Model.SelectedPage ).BasePresenter.CreateNewModel( 0 );

                foreach (var workspaceViewModel in Workspaces.
                    Where(w=> ! (w is ListViewViewModel) || (w is EditViewModel<T>) ).ToArray())
                {
                    Workspaces.Remove(workspaceViewModel);
                }

                SetActiveWorkSpace(0);
            }
        }

        public virtual void RefreshSharedViewsAfterAddedNew( BaseEntity updatedEntity )
        {
            ReferenceDataFactory.LazyRefresh( typeof( T ) );
        }

        public virtual void RefreshSharedViewsAfterUpdated( BaseEntity dto )
        {
            ReferenceDataFactory.LazyRefresh( typeof( T ) );

            var viewModel = Model.Workspaces[1] as EditViewModel<T>;

            if ( viewModel == null ) return;

            var entity = viewModel.CurrentEntity;

            if ( entity == null ) return;

            var index = Model.UserEntities.IndexOf( dto );

            Model.UserEntities[index] = dto;

            //refresh della list view quando ritorna attiva
            var tabViewModel = Model.Workspaces[0] as ListViewViewModel;
            if ( tabViewModel != null && tabViewModel.NeedRefresh == false )
                tabViewModel.NeedRefresh = true;
        }

        protected virtual void OnPageChanged( object o, PropertyChangeEventArgs data )
        {
            var tabViewModel = data.NewValue as ListViewViewModel;

            if ( tabViewModel == null ) return;

            var vm = tabViewModel as EditViewModel<T>;
            if ( vm != null)
                //default behavoir on page changed
            {
                vm.OnPageGotFocus();
            }
            else
            {
                if ( !tabViewModel.NeedRefresh ) return;
                Model.UserCollectionView.Refresh();
                tabViewModel.NeedRefresh = false;
            }
        }

        protected virtual void SetActiveWorkspace( WorkspaceViewModel workspace )
        {
            Model.WorkspacesCollectionView.MoveCurrentTo( workspace );
        }

        public virtual void SetActiveWorkSpace( int index )
        {
            if ( Model.Workspaces.Count <= index || index < 0)
                return;
            var workspace = Model.Workspaces[index];
            Model.WorkspacesCollectionView.MoveCurrentTo( workspace );
            Model.SelectedPage = workspace;
        }

        private void CreateNewEntityExecuted()
        {
            //show current entity is default behavoir OnPageChanged
            Model.SelectedPageChanged -= OnPageChanged;
            var editViewModel = GetEditViewModel();
            editViewModel?.AddNew();
            Model.SelectedPageChanged += OnPageChanged;
        }

        protected virtual void ApriSchedaDettaglioExecuted()
        {
            //set active or create new edit view model
            //show current entity is default behavoir on active workspace (OnPageChanged)
            GetEditViewModel();
        }

        private EditViewModel<T> GetEditViewModel()
        {
            EditViewModel<T> editViewModel = null;

            if ( Model.Workspaces.OfType<EditViewModel<T>>().Any() )
                editViewModel = ( EditViewModel<T> ) Model.EditViewModel;

            if ( editViewModel == null )
            {
                Model.SetEditViewModel( Session, this );
                editViewModel = ( EditViewModel<T> ) Model.EditViewModel;
                if ( editViewModel == null ) return null;
                Model.Workspaces.Add( Model.EditViewModel );
            }
            SetActiveWorkspace( editViewModel );
            return editViewModel;
        }

        private bool ApriSchedaDettalioCanExecute()
        {
            return Model.UserCollectionView != null && Model.UserCollectionView.CurrentItem != null;
        }
        
        protected bool IsIncrementalSearch()
        {
            if ( Model == null || Model.Workspaces == null ) return false;
            var editViewModel = ( EditViewModel<T> ) Model.Workspaces.FirstOrDefault( w => w is EditViewModel<T> );
            return editViewModel != null && editViewModel.IsIncrementalSearch;
        }

        private static void CheckForIncrementalResult( List<BaseEntity> entities, IEnumerable<BaseEntity> source )
        {
            var newIds = ( from entity in entities select entity.Id ).ToArray();
            foreach ( var baseEntity in source.Where( baseEntity => !newIds.Contains( baseEntity.Id ) ) )
            {
                entities.Add( baseEntity );
            }
        }

        protected virtual void SetUpNewModel( int activeTab, IList created )
        {
            if ( Model == null )
            {
                Model = CreateNewModel();
                Model.UserEntities = created;
                Model.UserCollectionView = CollectionViewSource.GetDefaultView( Model.UserEntities );
                Model.SetEditViewModel( Session, this );
                SetActiveWorkSpace( activeTab );
                Model.SelectedPageChanged += OnPageChanged;
                IsBusy = false;
            }
            else
            {
                Model.UserEntities = created;
                Model.UserCollectionView = CollectionViewSource.GetDefaultView( Model.UserEntities );
                
                //var viewModel = Model.EditViewModel as EditViewModel<T>;
                //if ( viewModel != null )
                //    viewModel.SetUpCollectionView( Model.UserEntities, Model.UserCollectionView );

                Model.UserCollectionView.MoveCurrentToFirst();
                Model.UserCollectionView.Refresh();
                IsBusy = false;
            }
        }

        protected virtual void OnConfirmResultFlyFetch<TE, TDto>( object sender, FinderConfirmSearchEventArgs data ) where
            TDto : new() where TE : class
        {
            AllowedGridProperties = data.AllowedGridProperties;
            IsBusy = true;
            if ( data.IsIdSelection )
            {
                List<long> allowedIds;
                var selectedIds = new List<long>();
                foreach ( BaseEntity newid in data.Collection )
                    selectedIds.Add( newid.Id );
                if ( IsIncrementalSearch() )
                {
                    var oldIds = new List<long>();
                    foreach ( BaseEntity entity in Model.UserEntities )
                        oldIds.Add( entity.Id );
                    allowedIds = oldIds.Union( selectedIds ).Distinct().ToList();
                }
                else
                    allowedIds = selectedIds;

                //if ((from id in allowedIds where id == 0 select id).Any())
                //    throw new Exception();

                //todo:crea un delegato e passa il metotod al posto di this
                var pageProvider1 = new GetByIdsQueryObject<TE, TDto, ObservableCollection<TDto>>( allowedIds, this );

                //pageProvider1.DetachedCriteria = DetachedCriteria.For<TE>();

                pageProvider1.DetachedCriteria = QueryCriteria.DetachedCriteria;

                const int pageSize = 100;
                CollectionFactory.Create( pageSize, pageProvider1, new GetbyIdsCountProvider( allowedIds.Count ),
                    created => SetUpNewModel( 0, created ) );
            }
            else
            {
                SetUpNewModel( 0, data.Collection );
            }
        }

        public override void Dispose()
        {
            Model.SelectedPageChanged -= OnPageChanged;
            base.Dispose();
        }

        public abstract void QueryInProgress( bool inProgress );



        protected void InitViewModel( QueryOver queryByExample )
        {
            var entities = GetExeCriteria<T>( queryByExample );

            SetUpNewModel( 0, new ObservableCollection<T>( entities ) );
        }

        protected void SetUpModel( IEnumerable<T> entitiesdto )
        {
            Model.UserEntities = new ObservableCollection<T>( entitiesdto );
            Model.UserCollectionView = CollectionViewSource.GetDefaultView( Model.UserEntities );
            Model.UserCollectionView.MoveCurrentToFirst();
        }

        #region command

        private ICommand _apriSchedaDettalio;
        public ICommand ApriSchedaDettalio
        {
            get
            {
                if ( _apriSchedaDettalio == null )
                {
                    _apriSchedaDettalio = new RelayCommand(
                        param => ApriSchedaDettaglioExecuted(),
                        param => ApriSchedaDettalioCanExecute()
                        );
                }
                return _apriSchedaDettalio;
            }

        }

        private ICommand _onGridEmptyCommand;

        public virtual ICommand OnGridEmptyCommand
        {
            get
            {
                if ( _onGridEmptyCommand == null )
                {
                    _onGridEmptyCommand = new RelayCommand( param => CreateNewEntityExecuted(), k => CanCreateNewEntity );
                }
                return _onGridEmptyCommand;
            }
        }

        protected virtual bool CanCreateNewEntity
        {
            get { return true; }

        }

        #endregion
    }
}
