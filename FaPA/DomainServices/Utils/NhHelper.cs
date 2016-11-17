using System;
using NHibernate;

namespace FaPA.DomainServices.Utils
{
    public class NhHelper
    {
        public UnitOfWork OpenUnitOfWork()
        {
            _factory = NHibernateStaticContainer.SessionFactory;
            return new UnitOfWork(_factory);
        }

        public void EvictAll<T>()
        {
            _factory.Evict(typeof(T));
        }

        private static NhHelper _instance;
        private static readonly object Padlock = new object();
        public static NhHelper Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (null == _instance)
                        _instance = new NhHelper();
                }
                return _instance;
            }
        }

        private ISessionFactory _factory;

        public ISession CurrentSession
        {
            get { return _factory.GetCurrentSession(); }
        }
    }

    public static class NhUtils
    {
        public static void ClearSessionFactoryQueries(ISessionFactory sessionFactory, Type type)
        {
            sessionFactory.EvictQueries();

            sessionFactory.Evict(type);

            foreach (var collectionMetadata in sessionFactory.GetAllCollectionMetadata())
                sessionFactory.EvictCollection(collectionMetadata.Key);

            foreach (var classMetadata in sessionFactory.GetAllClassMetadata())
                sessionFactory.EvictEntity(classMetadata.Key);
        }
    }
}

