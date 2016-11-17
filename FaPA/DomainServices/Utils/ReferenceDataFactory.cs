using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FaPA.Core;
using NHibernate;

namespace FaPA.DomainServices.Utils
{

    public class ReferenceDataFactory
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable> ReferenceDataList =
            new ConcurrentDictionary<Type, IEnumerable>();

        static readonly HashSet<Type> InvalidLists = new HashSet<Type>();

        private readonly string[] _eagerFetchProps;

        public ReferenceDataFactory()
        { }

        //used in xaml resource
        public ReferenceDataFactory(string className, string[] eagerFetchProps)
        {
            if (NHibernateStaticContainer.SessionFactory == null)
                return;
            _eagerFetchProps = eagerFetchProps;
            //get instance type name in xmal resources
            var instanceType = typeof(BaseEntity).Assembly.GetType(String.Format("Emule.Core.{0}", className));
            if (instanceType == null)
                return;
            ReferenceDataList.TryAdd(instanceType, GetReferenceCollection(instanceType));
        }

        public static void LazyRefresh(Type type)
        {
            InvalidLists.Add(type);
        }

        private IEnumerable GetReferenceCollection(Type instanceType)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetReferenceCollection<T>() where T : class
        {
            var instanceType = typeof(T);
            var isInvalid = InvalidLists.Contains(instanceType);
            if (ReferenceDataList.ContainsKey(instanceType) && ReferenceDataList[instanceType] != null && !isInvalid)
            {
                return (IList<T>)ReferenceDataList[instanceType];
            }

            var list = LoadReferenceList<T>();
            ReferenceDataList.TryAdd(instanceType, list);
            if (isInvalid)
            {
                InvalidLists.Remove(instanceType);
            }
            return (IList<T>)(list == null ? ReferenceDataList[instanceType] : list);
        }

        protected virtual IEnumerable<T> LoadReferenceList<T>() where T : class
        {
            IEnumerable<T> list;
            using (NhHelper.Instance.OpenUnitOfWork())
            {
                using (var tx = NhHelper.Instance.CurrentSession.BeginTransaction())
                {
                    var criteria = NhHelper.Instance.CurrentSession.CreateCriteria<T>();
                    if (_eagerFetchProps != null && _eagerFetchProps.Length > 0)
                    {
                        foreach (var eagerFetchProp in _eagerFetchProps)
                            criteria.SetFetchMode(eagerFetchProp, FetchMode.Join);
                    }
                    list = criteria.List<T>();
                    tx.Commit();
                }
            }
            return list;
        }

    }
    

}
