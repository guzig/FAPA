using System;
using System.Windows;
using System.Windows.Input;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Controls.MyTabControl
{

    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModelBase
    {

        #region Members

        private RelayCommand _closeCommand;

        public WorkspaceViewModel()
        {
            IsCloseable = true;
        }

        public bool IsCloseable { get; set; }

        #endregion // Fields

        #region Constructor

        #endregion // Constructor

        #region CloseCommand

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(param => OnRequestClose())); }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        public event EventHandler RequestClose;

        protected virtual void OnRequestClose()
        {
            if (!string.IsNullOrWhiteSpace(LockMessage))
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(LockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var handler = RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }


        #endregion // RequestClose [event]



    }
}
