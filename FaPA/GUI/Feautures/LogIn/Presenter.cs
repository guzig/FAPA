using System;
using System.Windows.Input;
using FaPA.GUI.Controls.LogOnMask;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.LogIn
{
    public class Presenter : AbstractPresenter<Model, View>
    {

        //public void Initialize()
        //{

        //}

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if ( _submitCommand != null ) return _submitCommand;
                _submitCommand = new RelayCommand( ExecuteSubmit, CanExecuteSubmit );
                return _submitCommand;
            }

        }

        public Presenter()
        {
            Model = new Model() {SubmitCommand = SubmitCommand};
        }

        private void ExecuteSubmit( object commandParameter )
        {
            var accessControlSystem = commandParameter as SmartLoginOverlay;
            
            try
            {
                Model.Login();

                if (accessControlSystem == null || !Model.IsAuthenticated ) return;

                accessControlSystem.Unlock();

                View.Close();
            }
            catch (Exception )
            {
                accessControlSystem.ShowWrongCredentialsMessage();
            }

        }

        private bool CanExecuteSubmit( object commandParameter )
        {
            return !string.IsNullOrEmpty( Model.UserName );
        }

    }
}