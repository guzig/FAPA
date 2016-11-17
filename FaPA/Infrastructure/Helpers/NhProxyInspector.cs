using System;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Validator.Engine;

namespace FaPA.Infrastructure.Helpers
{
    public class NhProxyInspector : IEntityTypeInspector
    {
        public Type GuessType(object entityInstance)
        {
            var proxy = entityInstance as IProxy;
            return proxy != null ? entityInstance.GetType().BaseType : null;
        }
    }


}