using System;
using NHibernate;
using NHibernate.Context;

namespace FaPA.DomainServices.Utils
{
    public class UnitOfWork : IDisposable
    {
        private readonly ISession _session;
        private readonly ISessionFactory _factory;
        public UnitOfWork(ISessionFactory factory)
        {
            _factory = factory;
            _session = _factory.OpenSession();
            CurrentSessionContext.Bind(_session);
        }
        #region IDisposable Members

        public void Dispose()
        {
            _session.Close();
            CurrentSessionContext.Unbind(_factory);
        }

        #endregion
    }
}
