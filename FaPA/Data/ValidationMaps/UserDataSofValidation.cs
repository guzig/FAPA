using FaPA.Core;
using NHibernate.Validator.Cfg.Loquacious;

namespace FaPA.Data.ValidationMaps
{
    public class UserDataSofValidation : ValidationDef<UserData>
    {
        public UserDataSofValidation()
        {
            Define( l => l.UserName ).NotNullableAndNotEmpty().WithMessage( "Specificare un nome utente" );
            Define( f => f.Email ).IsEmail();
        }
    }
}