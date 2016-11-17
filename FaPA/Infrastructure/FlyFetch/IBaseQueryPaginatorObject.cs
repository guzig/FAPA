using System;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.FlyFetch
{
    public interface IBaseQueryPaginatorObject
    {
        DetachedCriteria DetachedCriteria { get; set; }
        INotifyDataSourceHit NotifyHit { get; set; }
        Action<Exception> ReportError { get; set; }
        int Count { get; set; }
        bool IsFetchCompleted{get; }
    }
}