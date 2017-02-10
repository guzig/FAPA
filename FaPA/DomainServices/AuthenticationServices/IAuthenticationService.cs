using FaPA.Core;

namespace FaPA.DomainServices.AuthenticationServices
{
    public interface IAuthenticationService
    {
        UserData AuthenticateUser(string username, string password);
    }
}
