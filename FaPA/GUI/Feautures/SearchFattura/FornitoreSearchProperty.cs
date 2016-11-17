using FaPA.Infrastructure.Finder;
using NHibernate.SqlCommand;

namespace FaPA.GUI.Feautures.SearchFattura
{
    public class FornitoreSearchProperty : AssociationSearchProperty<Core.Fornitore>
    {

        public FornitoreSearchProperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            RequiredJoin = JoinType.InnerJoin;
        }
    }
}