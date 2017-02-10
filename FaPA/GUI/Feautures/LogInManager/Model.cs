using System;
using System.Text;
using System.Windows.Input;
using Caliburn.Micro;
using FaPA.Annotations;
using FaPA.DomainServices.AuthenticationServices;

namespace FaPA.GUI.Feautures.LogInManager
{
    public class Model : PropertyChangedBase
    {
        private readonly CustomIdentity _identity;

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if ( value == _userName ) return;
                _userName = value;
                NotifyOfPropertyChange( () => UserName );
            }
        }

        //private string _currentPassword;
        //public string CurrentPassword
        //{
        //    get { return _currentPassword; }
        //    set
        //    {
        //        if (value == _currentPassword) return;
        //        _currentPassword = value;
        //        NotifyOfPropertyChange(() => CurrentPassword);
        //    }
        //}

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if ( value == _password ) return;
                _password = value;
                NotifyOfPropertyChange( () => Password );
            }
        }

        private string _confirmedPassword;
        public string ConfirmedPassword
        {
            get { return _confirmedPassword; }
            set
            {
                if (value == _confirmedPassword) return;
                _confirmedPassword = value;
                NotifyOfPropertyChange(() => ConfirmedPassword);
            }
        }

        private string _status;

        private string _errors;
        public string Errors
        {
            get { return _errors; }

            set
            {
                if (value == _errors) return;
                _errors = value;
                NotifyOfPropertyChange(() => Errors);
            }
        }


        public Model([NotNull] ICommand submitCommand, ICommand saveCommand, string name)
        {
            if ( submitCommand == null || saveCommand == null || string.IsNullOrWhiteSpace(name) )
                throw new Exception( );
            
            UserName = name;

            SubmitCommand = submitCommand;

            SaveCommand = saveCommand;
        }


        public ICommand SaveCommand { get; set; }

        public ICommand SubmitCommand { get; set; }

        public void Validate()
        {
            Errors = null;
            var sb = new StringBuilder();
            
            if ( string.IsNullOrWhiteSpace(Password))
            {
                if (sb.Length > 0)
                    sb.Append(Environment.NewLine);
                    
                sb.Append( "Non è stata digitata una nuova password." );

                IsValid = false;
                Errors = sb.ToString();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(ConfirmedPassword))
            {
                if ( sb.Length > 0 )
                    sb.Append( Environment.NewLine );

                sb.Append( "Non è stata digitata la password di conferma." );
                IsValid = false;
                Errors = sb.ToString();
                return;
            }

            if (Password != ConfirmedPassword)
            {
                if ( sb.Length > 0 )
                    sb.Append( Environment.NewLine );

                sb.Append( "La nuova password e quella di conferma non sono uguali." );
                IsValid = false;
                Errors = sb.ToString();
                return;
            }

            Errors = null;
            IsValid = true;

        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                NotifyOfPropertyChange(() => IsValid);

                ErrorsMsgVisibility = IsValid == false ? "Visible" : "Collapsed";
            }
        }

        private string _errorsMsgVisibility="Collapsed";
        public string ErrorsMsgVisibility
        {
            get { return _errorsMsgVisibility; }
            set
            {
                _errorsMsgVisibility = value;
                NotifyOfPropertyChange(() => ErrorsMsgVisibility);
            }
        }


    }
}