using FaPA.Core;
using FaPA.DomainServices.Utils;

namespace FaPA.DomainServices.AuthenticationServices
{
    public static class AuthDbHelpers
    {
        public static UserData TryGetUser(string name)
        {
            UserData user;

            using (NHhelper.Instance.OpenUnitOfWork())
            {
                using (var tx = NHhelper.Instance.CurrentSession.BeginTransaction())
                {
                    user = NHhelper.Instance.CurrentSession.QueryOver<UserData>().
                        Where(u => u.UserName == name.ToUpper()).SingleOrDefault<UserData>();
                    tx.Commit();
                }
            }

            return user;
        }
        
    }
}
