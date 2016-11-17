using FaPA.Infrastructure.Finder;
using NHibernate.SqlCommand;

namespace FaPA.GUI.Feautures.SearchFattura
{
    public class CommittenteSearchProperty : AssociationSearchProperty<Core.Committente>
    {

        public CommittenteSearchProperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            RequiredJoin = JoinType.InnerJoin;
        }
    }
}