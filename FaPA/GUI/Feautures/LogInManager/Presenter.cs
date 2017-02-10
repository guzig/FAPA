using System;
using System.Threading;
using System.Windows.Input;
using FaPA.DomainServices.AuthenticationServices;
using FaPA.DomainServices.Utils;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.LogInManager
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

        private ICommand _saveCommand;
        private readonly CustomIdentity _identity;

        public ICommand SaveCommand
        {
            get
            {
                if ( _saveCommand != null ) return _saveCommand;
                _saveCommand = new RelayCommand( ExecuteSave, x => true );
                return _saveCommand;
            }
        }

        public Presenter()
        {

            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;

            if ( customPrincipal == null || customPrincipal.Identity is AnonymousIdentity)
                throw new ArgumentException( "Privilegi insufficenti" );

            //Authenticate the user
            _identity = customPrincipal.Identity;

            Model = new Model( SubmitCommand, SaveCommand, _identity.Name );
        }

        private void ExecuteSubmit( object commandParameter )
        {
            //var accessControlSystem = commandParameter as SmartLoginOverlay;

            try
            {
                Model.Validate();

                //if ( accessControlSystem == null || !Model.IsAuthenticated ) return;

                //accessControlSystem.Unlock();

                //View.Close();
            }
            catch ( Exception )
            {
                //accessControlSystem.ShowWrongCredentialsMessage();
            }

        }

        private bool CanExecuteSubmit( object commandParameter )
        {
            return true;
            //return  !string.IsNullOrEmpty( Model.Password ) && !string.IsNullOrEmpty( Model.ConfirmedPassword ) && 
            //    Model.Password==Model.ConfirmedPassword;
        }

        private void ExecuteSave( object obj )
        {
            //var hashedPassword = PasswordHasher.HashPassword( _identity.Name, Model.Password );
            try
            {
                _identity.User.SetCredentials( _identity.Name, Model.Password );

                using ( NHhelper.Instance.OpenUnitOfWork() )
                {
                    using ( var tx = NHhelper.Instance.CurrentSession.BeginTransaction() )
                    {
                        NHhelper.Instance.CurrentSession.Update( _identity.User );
                        NHhelper.Instance.CurrentSession.Flush();
                        tx.Commit();
                    }
                }
            }
            catch (Exception )
            {
                WpfHelpers.ShowErrorSavingEntityMsg();
            }

            View.Close();
        }
    }
}