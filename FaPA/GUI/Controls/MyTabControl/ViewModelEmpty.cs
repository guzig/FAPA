using System;
using System.Windows.Input;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Controls.MyTabControl
{
    public class ViewModelEmpty:WorkspaceViewModel
    {

        public Action Action { get; set; }


        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message) return;
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }


        public string IsVisibleCommand
        {
            get { return string.IsNullOrEmpty(_commandText) ? "Hydden" : "Visible"; }
        }

        private string _commandText = "";
        public string CommandText
        {
            get { return _commandText; }
            set
            {
                if (value == _commandText) return;
                _commandText = value;
                NotifyOfPropertyChange(() => CommandText);
                NotifyOfPropertyChange(() => IsVisibleCommand);
            }
        }

        private ICommand _commandBehavoir;
        public ICommand CommandBehavoir
        {
            get
            {
                return _commandBehavoir ?? (_commandBehavoir =
                    new RelayCommand(param => DeleteEntityExecuted()));
            }
        }

        private void DeleteEntityExecuted()
        {
            if (Action !=null)
                Action.Invoke();
        }

    }
}