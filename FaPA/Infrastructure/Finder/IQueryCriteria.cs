using System.Collections.Generic;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{
    public interface IQueryCriteria
    {
        //string Path { get;  }
        void GetQueryCriteria(DetachedCriteria detachedQueryCriteria, string parent);
        IEnumerable<string> GetBrokenRules(string propName);
        bool HasCriteria();
        void ClearSearchParamValue();
        string Validate();
    }
}