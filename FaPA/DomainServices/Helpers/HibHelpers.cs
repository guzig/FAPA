using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Util;
using NhValEnvironment = NHibernate.Validator.Cfg.Environment;

namespace FaPA.DomainServices.Helpers
{

    public static class HibHelpers
    {
        public static IList<T> BatchLoadEntitiesByIds<T>(ISession session, string sql,
            IList<long> ids, string paramName, int pageSize)
        {
            //string.Format("from {0} f where f.Id = :id", typeof(T).Name);

            if (ids == null || sql == null) throw new ArgumentNullException();

            var flushMode = session.FlushMode;
            session.FlushMode = FlushMode.Never;

            const int batchSize = 100;
            session.SetBatchSize(batchSize);

            var multiQuery = session.CreateMultiQuery();

            var pageSizeOrDefault = pageSize == 0 ? 100 : pageSize;
            int pageCount = CountPages(ids.Count, pageSizeOrDefault);
            int index = 0;
            for (var pageId = 0; pageId < pageCount & index < ids.Count; pageId++)
            {
                var pagedIds = new List<long>();
                for (var i = 0; i < pageSizeOrDefault & index < ids.Count; i++)
                {
                    pagedIds.Add(ids[index++]);
                }
                multiQuery.Add(session.CreateSQLQuery(sql).SetParameterList(paramName, pagedIds));
            }
            IList queryResults;
            using (var tx = session.BeginTransaction())
            {
                queryResults = multiQuery
                    .List();
                tx.Commit();
            }

            session.SetBatchSize(50);
            session.FlushMode = flushMode;

            var results = new List<T>();
            if (!queryResults.Any())
                return results;

            foreach (object list in queryResults.Cast<object>().ToList())
            {
                var page = list as IList<object>;
                results.AddRange(from object item in page select (T)item);
                //results.AddRange(page.Cast<object>().Select(entity => (T) entity));
            }

            return results;
        }

        public static IList<T> BatchLoadEntitiesByIds<T>(DetachedCriteria criteria, IList<long> ids, int pageSize,
            ISession session, bool cacheable)
        {
            //string.Format("from {0} f where f.Id = :id", typeof(T).Name);

            if (ids == null || criteria == null) throw new ArgumentNullException();

            var flushMode = session.FlushMode;
            session.FlushMode = FlushMode.Never;

            const int batchSize = 100;
            session.SetBatchSize(batchSize);

            var multiQuery = session.CreateMultiCriteria();

            var pageSizeOrDefault = pageSize == 0 ? 100 : pageSize;
            int pageCount = CountPages(ids.Count, pageSizeOrDefault);
            int index = 0;
            for (var pageId = 0; pageId < pageCount & index < ids.Count; pageId++)
            {
                var pagedIds = new List<long>();
                for (var i = 0; i < pageSizeOrDefault & index < ids.Count; i++)
                {
                    pagedIds.Add(ids[index++]);
                }
                multiQuery.Add(CriteriaTransformer.Clone(criteria).Add(Restrictions.In("Id", pagedIds)).
                    SetCacheMode(CacheMode.Normal).SetCacheable(cacheable));
            }
            IList queryResults;
            using (var tx = session.BeginTransaction())
            {
                queryResults = multiQuery
                    //SetCacheable( false )
                    .List();
                tx.Commit();
            }

            session.SetBatchSize(50);
            session.FlushMode = flushMode;

            var results = new List<T>();
            if (!queryResults.Any())
                return results;

            foreach (object list in queryResults.Cast<object>().ToList())
            {
                var page = list as IList<object>;
                results.AddRange(from object item in page select (T)item);
                //results.AddRange(page.Cast<object>().Select(entity => (T) entity));
            }

            return results;
        }

        public static int CountPages(int totalRecords, int recordsPerPage)
        {
            return (totalRecords - 1) / recordsPerPage + 1;
        }

        private static ValidatorEngine _validator;
        public static ValidatorEngine Validator
        {
            get
            {
                return _validator ??
                       (_validator = NhValEnvironment.SharedEngineProvider.GetEngine());
            }
        }

        public static void ClearAndDisconnectSession(this ISession session)
        {
            if (session == null)
                return;

            session.Clear();

            if (session.IsConnected)
            {
                session.Disconnect();
            }
        }

        public static void ReconnectSession(this ISession session)
        {
            if (!session.IsConnected)
            {
                session.Reconnect();
            }
        }
    }

}