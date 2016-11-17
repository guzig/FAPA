using System;
using System.ComponentModel;
using NHibernate;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.Data
{
    public class NotifyPropChangedAndDataErrorInfoInterceptor : EmptyInterceptor
    {
        private ISession _session;
        private static readonly ProxyFactory Factory = new ProxyFactory();

        public override void SetSession(ISession session)
        {
            this._session = session;
            base.SetSession(session);
        }

        public override object Instantiate(string clazz, EntityMode entityMode, object id)
        {
            var entityType = Type.GetType(clazz);
            var proxy = Factory.CreateProxy(entityType, new PropChangedAndDataErrorDynProxyInterceptor(), 
                typeof(INotifyPropertyChanged), typeof(INotifyDataErrorInfo) ) as IProxy;

            var interceptor = proxy.Interceptor as PropChangedAndDataErrorDynProxyInterceptor;

            interceptor.Proxy = this._session.SessionFactory.GetClassMetadata(entityType).Instantiate(id, entityMode);

            this._session.SessionFactory.GetClassMetadata(entityType).SetIdentifier(proxy, id, entityMode);

            return proxy;
        }
    }
}
