using FaPA.Core;
using FaPA.DomainServices.Helpers;
using FaPA.Infrastructure.Helpers;

namespace FaPA.Data
{
    using System;
    using System.ComponentModel;
    using NHibernate.Proxy.DynamicProxy;

    public class AddPropChangedAndDataErrorInterceptorProxyFactory
    {
        private static readonly ProxyFactory _factory = new ProxyFactory();

        public static TEntityType Create<TEntityType>(PropertyChangedEventHandler onChangedHanler=null) 
            where TEntityType : new()
        {
            return (TEntityType)Create(typeof(TEntityType), new TEntityType());
        }

        public static object Create(Type entityType, object entity)
        {
            PropertyChangedEventHandler changedEventHandler = null;

            if ( entity is BaseEntity )
            {
                changedEventHandler = entity.TryGetPropChangedEventHandler();
            }
            
            if ( entity is IProxy )
            {
                //throw new Exception("E' gia' un proxy");
                return entity;
            }

            var proxy = _factory.CreateProxy(entityType, new PropChangedAndDataErrorDynProxyInterceptor(entity, 
                changedEventHandler ), typeof(INotifyPropertyChanged), typeof(INotifyDataErrorInfo) );

            return proxy;
        }
    }
}
