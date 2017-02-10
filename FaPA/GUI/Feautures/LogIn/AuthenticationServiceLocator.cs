using System.Threading;
using FaPA.Core;
using FaPA.DomainServices.AuthenticationServices;

namespace FaPA.GUI.Feautures.LogIn
{
    public static class AuthenticationServiceLocator
    {
        private static IAuthenticationService _authenticationService;

        public static IAuthenticationService Service
        {
            get
            {
                if ( _authenticationService == null )
                    _authenticationService = new AuthenticationService();

                return _authenticationService;
            }
        }

        public static bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        public static bool IsAdministrator
        {
            get { return Thread.CurrentPrincipal.IsInRole(TipoUtenteEnums.Administrators.ToString()); }
        }
    }
}