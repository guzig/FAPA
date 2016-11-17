using NHibernate;

namespace FaPA.DomainServices
{
    public static class NHibernateStaticContainer
    {

        public static ISessionFactory SessionFactory { get; set; }

        private static ISession _session;
        public static ISession Session
        {
            get
            {
                if (_session == null)
                    _session = SessionFactory.OpenSession();
                return _session;
            }

        }
    }
}