using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using FaPA.Infrastructure.Utils;
using NHibernate;

namespace FaPA.GUI.Controls.MyTabControl
{
    public abstract class ModelBase : PropertyChangedBase, IDisposable
    {
        public abstract string DisplayName { get; }
        
        #region user collection enetities to show and submit to crud operation

        private ICollectionView _userCollectionView;
        public ICollectionView UserCollectionView
        {
            get { return _userCollectionView; }
            set
            {
                _userCollectionView = value;
                NotifyOfPropertyChange( () => UserCollectionView );
                UserCollectionView.MoveCurrentToFirst();
            }
        }

        public abstract IList UserEntities { get; set; }

        #endregion

        #region Workspaces 

        /// <summary>
        /// Workspace is a ViewModel used to manipulate user collection
        /// by default we have a Workspace to show entities list filter for editing or other manipulation
        /// and another workspace for the operation the CRUD operaration of a selected item...
        /// other workspace can be added as context for other operation on a current item selected
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces { get; private set; }

        public ICollectionView WorkspacesCollectionView { get; private set; }
        
        /// <summary>
        /// Default mandatory workspace for CRUD operation
        ///  </summary>
        public abstract WorkspaceViewModel EditViewModel { get; }


        private WorkspaceViewModel _selectedPage;
        /// <summary>
        /// Current workspace to host manipulation to the selected item
        /// </summary>
        public WorkspaceViewModel SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                if ( value != null && value.Equals( _selectedPage ) )
                    return;

                var oldValue = _selectedPage;

                _selectedPage = value;

                var handler = SelectedPageChanged;
                if ( handler != null )
                    handler( SelectedPageChanged, new PropertyChangeEventArgs( "SelectedPage", oldValue, _selectedPage ) );
            }
        }
        #endregion
        

        public delegate void PropertyChangeHandler( object sender, PropertyChangeEventArgs data );
        public event PropertyChangeHandler SelectedPageChanged;


        //ctor
        protected ModelBase()
        {
            Workspaces = new ObservableCollection<WorkspaceViewModel>();
            Workspaces.CollectionChanged += OnWorkspacesChanged;
            WorkspacesCollectionView = CollectionViewSource.GetDefaultView(Workspaces);
        }


        /// <summary>
        /// Setup default mandatory workspace for CRUD operation
        /// </summary>
        /// <param name="session"></param>
        /// <param name="basePresenter"></param>
        public abstract void SetEditViewModel( ISession session, IBasePresenter basePresenter );


        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Cast<WorkspaceViewModel>().Count(w => w!=null) != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += OnWorkspaceRequestClose;
                }


            if (e.OldItems != null && e.OldItems.Cast<WorkspaceViewModel>().Count(w => w != null) != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= OnWorkspaceRequestClose;
                }

            WorkspacesCollectionView = CollectionViewSource.GetDefaultView(Workspaces);            
        }

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            var workspace = sender as WorkspaceViewModel;
            
            if (workspace == null) return;
            
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

        #endregion // Workspaces


        void IDisposable.Dispose()
        {
            foreach ( var f in this.Workspaces )
                f.Dispose();

            this.Workspaces.Clear();
        }
    }
}