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
    public abstract class BaseCrudModel : PropertyChangedBase
    {
        public abstract WorkspaceViewModel EditViewModel { get; }

        public abstract void SetEditViewModel(ISession session, IBasePresenter basePresenter);

        private ICollectionView _userCollectionView;
        public ICollectionView UserCollectionView
        {
            get { return _userCollectionView; }
            set
            {
                _userCollectionView = value;
                NotifyOfPropertyChange(() => UserCollectionView);
                UserCollectionView.MoveCurrentToFirst();
            }
        }

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; private set; }

        public ICollectionView WorkspacesCollectionView { get; private set; }

        public abstract IList UserEntities { get; set; }

        public abstract string DisplayName { get; }

        protected BaseCrudModel()
        {
            Workspaces = new ObservableCollection<WorkspaceViewModel>();
            Workspaces.CollectionChanged += OnWorkspacesChanged;
            WorkspacesCollectionView = CollectionViewSource.GetDefaultView(Workspaces);
        }
        
        public delegate void PropertyChangeHandler(object sender, PropertyChangeEventArgs data);
        public event PropertyChangeHandler SelectedPageChanged;

        private WorkspaceViewModel _selectedPage;
        public WorkspaceViewModel SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                if (value != null && value.Equals(_selectedPage))
                    return;

                var oldValue = _selectedPage;
                
                _selectedPage = value;

                var handler = SelectedPageChanged;
                if (handler != null)
                    handler(SelectedPageChanged, new PropertyChangeEventArgs("SelectedPage", oldValue, _selectedPage));
            }
        }
        
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
    }
}