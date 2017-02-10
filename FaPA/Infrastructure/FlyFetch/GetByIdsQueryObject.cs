using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FaPA.DomainServices.Utils;
using NHibernate;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.FlyFetch
{

    public class GetByIdsQueryObject<T, TDto, TColl> : BaseQueryPaginatorObject<T,TDto>, IGetByIdsQueryObject, 
        IPageProvider<TDto, TColl> where TDto : new() where TColl : IList<TDto>, new() where T : class
    {
        private readonly  List<long> _allowedIds;

        public List<long> AllowedIds
        {
            get { return _allowedIds; }
        }

        public GetByIdsQueryObject(List<long> ids, INotifyDataSourceHit notifyHit): base(notifyHit)
        {
            _allowedIds = ids;
        }

        #region IPageProvider<ViewItem,ObservableCollection<ViewItem>> Members

        public void GetAPage(TColl collection, int first, int count)
        {
            var wrk = new BackgroundWorker();
            wrk.DoWork += (s, e) =>
            {
                NotifyHit.QueryInProgress(true);
                try
                {
                    var ids = AllowedIds.Skip(first).Take(count).ToList();
                    IList<T> list;
                    using ( NHhelper.Instance.OpenUnitOfWork())
                    {
                        NHhelper.Instance.CurrentSession.FlushMode = FlushMode.Never;
                        using (var tx = NHhelper.Instance.CurrentSession.BeginTransaction())
                        {
                            var criteria = CriteriaTransformer.Clone(DetachedCriteria);
                            list = criteria.GetExecutableCriteria( NHhelper.Instance.CurrentSession)
                                            .Add(Restrictions.In("Id", ids))
                                            .SetReadOnly(true)
                                // :( -> when caching a query, FetchModes for related entites are not used
                                            .SetCacheable(false)
                                            .SetCacheMode(CacheMode.Normal)
                                            .List<T>();
                            tx.Commit();
                        }
                        e.Result = list;
                    }

                    CopyCollection(collection as ObservableCollection<T>, first, list);

                    //for (var i = 0; i < list.Count; ++i)
                    //{
                    //    Mapper.Map(list[i], collection[first + i]);
                    //}

                    FetchedCount += list.Count;
                }
                catch (Exception exc)
                {
                    ReportError(exc);
                }
            };

            wrk.RunWorkerCompleted += (s, e) =>
            {
                var entities = e.Result as IList<T>;

                FetchedCount += entities.Count;

                if (FetchedCount >= Count)
                {
                    IsFetchCompleted = true;
                    if (NotifyDataSourceLoadCompleted != null)
                        NotifyDataSourceLoadCompleted.LoadCompleted(true);
                }

                Completed(this, EventArgs.Empty);

                NotifyHit.QueryInProgress(false);
            };
            wrk.RunWorkerAsync();
        }

        public new event EventHandler Completed = delegate { };

        #endregion
    }
}