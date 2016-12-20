using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using NHibernate;
using NHibernate.Criterion;
using FaPA.DomainServices.Utils;

namespace FaPA.Infrastructure.FlyFetch
{
    public class BaseQueryPaginatorObject<T,TDto> : IBaseQueryPaginatorObject where T : class
    {
        public DetachedCriteria DetachedCriteria { get; set; }
        public INotifyDataSourceHit NotifyHit{ get; set; }
        public INotifyDataSourceLoadCompleted NotifyDataSourceLoadCompleted { get; set; }
        public Action<Exception> ReportError{ get; set; }
        //public ISession Session{ get; set; }
        protected int FetchedCount;
        public int Count { get; set; }
        public bool IsFetchCompleted { get; protected set; }

        protected BaseQueryPaginatorObject(INotifyDataSourceHit notifyHit)
        {
            NotifyHit = notifyHit;
            ReportError = OnReportError;
        }

        protected BaseQueryPaginatorObject(INotifyDataSourceHit notifyHit, 
            INotifyDataSourceLoadCompleted notifyDataSourceLoad):this(notifyHit)
        {
            NotifyDataSourceLoadCompleted = notifyDataSourceLoad;
        }

        #region ICountProvider Members

        public virtual void GetCount()
        {
            var wrk = new BackgroundWorker();

            wrk.DoWork += (s, e) =>
            {
                try
                {
                    IsFetchCompleted = false;
                    FetchedCount = 0;
                    if (NotifyDataSourceLoadCompleted != null)
                        NotifyDataSourceLoadCompleted.LoadCompleted(false);
                    NotifyHit.QueryInProgress(true);
                    using ( NhHelper.Instance.OpenUnitOfWork())
                    {
                        var criteria = DetachedCriteria.GetExecutableCriteria( NhHelper.Instance.CurrentSession);
                        Count = CriteriaTransformer.TransformToRowCount(criteria).UniqueResult<int>();
                    }
                    e.Result = Count;
                }
                catch (Exception exc)
                {
                    e.Result = 0;
                    ReportError(exc);
                }
            };
            wrk.RunWorkerCompleted += (s, e) =>
            {
                CountAvailable(this, new CountEventArgs((int)e.Result));
                NotifyHit.QueryInProgress(false);
            };
            wrk.RunWorkerAsync();
        }

        public event EventHandler<CountEventArgs> CountAvailable = delegate { };

        #endregion

        #region IPageProvider<ViewItem,ObservableCollection<ViewItem>> Members

        public virtual void GetAPage(ObservableCollection<TDto> collection, int first, int count)
        {
            var wrk = new BackgroundWorker();
            wrk.DoWork += (s, e) =>
            {
                NotifyHit.QueryInProgress(true);
                try
                {
                    IList<T> list;
                    using ( NhHelper.Instance.OpenUnitOfWork())
                    {
                        using (var tx = NhHelper.Instance.CurrentSession.BeginTransaction())
                        {
                            list = DetachedCriteria
                                .GetExecutableCriteria( NhHelper.Instance.CurrentSession )
                                .SetReadOnly(true)
                                // :( -> when caching a query, FetchModes for related entites are not used
                                //.SetCacheable(false)
                                //.SetCacheMode(CacheMode.Normal)
                                .SetFirstResult(first)
                                .SetMaxResults(count)
                                .List<T>();

                            tx.Commit();
                            e.Result = list;
                        }
                    }

                    CopyCollection(collection as ObservableCollection<T>, first, list);

                    //if (typeof (T) == typeof (TDto))
                    //{
                    //    CopyCollection(collection as ObservableCollection<T>, first, list);
                    //    throw new NotSupportedException();
                    //}
                    //else
                    //    MapToCollection(collection, first, list);
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
                    NotifyDataSourceLoadCompleted?.LoadCompleted(true);
                }
                Completed(this, EventArgs.Empty);
                NotifyHit.QueryInProgress(false);
            };
            wrk.RunWorkerAsync();
        }

        //private static void MapToCollection(ObservableCollection<TDto> collection, int first, IList<T> list)
        //{
        //    for (var i = 0; i < list.Count; ++i)
        //    {
        //        var entity = list[i];
        //        var dto = collection[first + i];
        //        Mapper.Map(entity, dto);
        //    }
        //}

        protected static void CopyCollection(ObservableCollection<T> collection, int first, IList<T> list)
        {
            for (var i = 0; i < list.Count; ++i)
            {
                collection[first + i]=list[i];
            }
        }

        public event EventHandler Completed = delegate { };

        #endregion

        private static void OnReportError(Exception exc)
        {
            Execute.OnUIThread(() => MessageBox.Show(GetMessage(exc), "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error));
        }

        private static string GetMessage(Exception exc)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Error occured:");
            do
            {
                sb.AppendLine(exc.Message);
                exc = exc.InnerException;
            } while (exc != null);
            return sb.ToString();
        }
    }
}