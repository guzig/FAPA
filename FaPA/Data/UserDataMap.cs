using FaPA.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace FaPA.Data
{
    public class UserDataMap : ClassMapping<UserData>
    {
        public UserDataMap()
        {
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.HighLow, gmap => gmap.Params(new { max_low = 100 }));
            });

            Property(x => x.HashedPassword, x =>
            {
                x.Length( 256 );
            });

            Property(x => x.UserName, x =>
            {
                x.Length(35);
                x.NotNullable(true);
                x.Unique(true);
            });

            Property(x => x.Role, x =>
            {
                x.Length(int.MaxValue);
            });

            Property(x => x.Claims, x =>
            {
                x.Length(int.MaxValue);
            });

            Property(x => x.Preferences, x =>
            {
                x.Length(int.MaxValue);
            });

            Property(x => x.PasswordExpiration, x =>
            {
                x.Length(int.MaxValue);
            });

            Property(x => x.Email, x =>
            {
                x.Length(int.MaxValue);
                x.Length(50);
            });

        }

    }
}