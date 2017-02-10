using System;
using System.Threading;
using System.Windows.Input;
using Caliburn.Micro;
using FaPA.DomainServices.AuthenticationServices;

namespace FaPA.GUI.Feautures.LogIn
{
    public class Model : PropertyChangedBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        
        private string _status;
        private ICommand _submitCommand;

        #region Properties

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Utente corrente {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Administrators") ? "( Amministratore )" : "");

                return "Non autenticato!";
            }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyOfPropertyChange(() => Status); }
        }
        #endregion

        //ctor
        public Model()
        {}

        public void Login()
        {
            try
            {
                //Validate credentials through the authentication service
                var user = AuthenticationServiceLocator.Service.AuthenticateUser(UserName, Password);

                //Get the current principal object
                var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity( user );

                Status = string.Empty;

            }
            catch (UnauthorizedAccessException)
            {
                Status = "Autenticazione non riuscita! Fornire credenziali valide.";
                throw new UnauthorizedAccessException("Accesso negato: password non valida.");
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
                throw new Exception("Errore durante lautenticazione");
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        private void Logout(object parameter)
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null) return;
            customPrincipal.Identity = new AnonymousIdentity();
            NotifyOfPropertyChange(() => AuthenticatedUser);
            NotifyOfPropertyChange(() => IsAuthenticated);
            //_loginCommand.RaiseCanExecuteChanged();
            //_logoutCommand.RaiseCanExecuteChanged();
            Status = string.Empty;
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        public ICommand SubmitCommand
        {
            get { return _submitCommand; }
            set { _submitCommand = value; }
        }

        //private void ShowView(object parameter)
        //{
        //    try
        //    {
        //        Status = string.Empty;
        //        IView view;
        //        if (parameter == null)
        //            view = new SecretWindow();
        //        else
        //            view = new AdminWindow();

        //        view.Show();
        //    }
        //    catch (SecurityException)
        //    {
        //        Status = "You are not authorized!";
        //    }
        //}

    }
}
