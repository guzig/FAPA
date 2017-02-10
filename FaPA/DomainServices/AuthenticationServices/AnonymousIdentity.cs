using FaPA.Core;

namespace FaPA.DomainServices.AuthenticationServices
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity(): base( new UserData()){ }
    }
}