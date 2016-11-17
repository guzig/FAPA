using NHibernate.SqlCommand;

namespace FaPA.Infrastructure.Finder
{
    public interface ISearchProperty : IQueryCriteria
    {
        ObjectFinder RootFinder { get;  }
        string PropertyName { get;  }
        //Type PropType { get;  }
        JoinType RequiredJoin { get; }
    }
}