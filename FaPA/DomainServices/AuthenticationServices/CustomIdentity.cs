using System.Security.Principal;
using FaPA.Core;

namespace FaPA.DomainServices.AuthenticationServices
{
    public class CustomIdentity : IIdentity
    {
        private readonly UserData _user;

        public CustomIdentity(UserData user)
        {
            _user = user;
        }

        public UserData User
        {
            get { return _user; }
        }

        public string Name { get { return _user.UserName; } }


        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
