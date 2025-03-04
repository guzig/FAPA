using System;
using FaPA.Data;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Validator.Engine;

namespace FaPA.DomainServices.Helpers
{
    public class NhProxyInspector : IEntityTypeInspector
    {
        public Type GuessType(object entityInstance)
        {
            var proxy = entityInstance as IProxy;
            return proxy != null ? entityInstance.GetType().BaseType : null;
        }

        public object Unproxy( object entityInstance )
        {
            var proxy = entityInstance as IProxy;
            if ( proxy == null ) return entityInstance;
            var instanc = proxy.Interceptor as PropChangedAndDataErrorDynProxyInterceptor;
            return instanc != null ? instanc.Proxy : entityInstance;
        }
    }


}