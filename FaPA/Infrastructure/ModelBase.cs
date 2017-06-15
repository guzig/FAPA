using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Caliburn.Micro;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure.Utils;

namespace FaPA.Infrastructure
{
    public class ModelBase : PropertyChangedBase
    {
        #region Workspaces

        private ObservableCollection<WorkspaceViewModel> _workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get { return _workspaces; }
            set
            {
                _workspaces = value;
                _workspaces.CollectionChanged += OnWorkspacesChanged;
                NotifyOfPropertyChange(() => Workspaces);
            }
        }

        private ICollectionView _workspacesCollectionView;
        public ICollectionView WorkspacesCollectionView
        {
            get { return _workspacesCollectionView; }
            set
            {
                _workspacesCollectionView = value;
                NotifyOfPropertyChange(() => WorkspacesCollectionView);
            }
        }

        public delegate void PropertyChangeHandler( object sender, PropertyChangeEventArgs data );
        public event PropertyChangeHandler SelectedPageChanged;

        private WorkspaceViewModel _selectedPage;
        public WorkspaceViewModel SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                var oldValue = _selectedPage;
                _selectedPage = value;

                var handler = SelectedPageChanged;
                if ( handler != null )
                    handler( SelectedPageChanged, new PropertyChangeEventArgs( "SelectedPage", oldValue, _selectedPage ) );
            }
        }

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        void OnWorkspacesChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null && e.NewItems.Count != 0 )
                foreach ( WorkspaceViewModel workspace in e.NewItems )
                {
                    workspace.RequestClose += OnWorkspaceRequestClose;
                    //workspace.Selected += this.OnW;
                }


            if ( e.OldItems != null && e.OldItems.Count != 0 )
                foreach ( WorkspaceViewModel workspace in e.OldItems )
                {
                    workspace.RequestClose -= OnWorkspaceRequestClose;
                    //workspace.Selected -= this.OnW;
                }
        }

        void OnWorkspaceRequestClose( object sender, EventArgs e )
        {
            var workspace = sender as WorkspaceViewModel;

            if ( workspace == null ) return;

            workspace.Dispose();
            Workspaces.Remove( workspace );
        }

        #endregion // Workspaces

        private ICommand _closeView;
        public ICommand CloseView
        {
            get { return _closeView; }
            set
            {
                if (Equals(value, _closeView)) return;
                _closeView = value;
                NotifyOfPropertyChange(() => CloseView);
            }
        }

        public ModelBase()
        {
            Workspaces = new ObservableCollection<WorkspaceViewModel>();
            WorkspacesCollectionView = CollectionViewSource.GetDefaultView(Workspaces);
        }

    }
}