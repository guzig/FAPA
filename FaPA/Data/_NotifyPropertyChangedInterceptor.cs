using System.ComponentModel;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.Data
{
    class _NotifyPropertyChangedInterceptor : NHibernate.Proxy.DynamicProxy.IInterceptor
    {
        private PropertyChangedEventHandler _changed = delegate { };

        public object Proxy { get; set; }

        #region IInterceptor Members

        public object Intercept(InvocationInfo info)
        {
            var isSetter = info.TargetMethod.Name.StartsWith("set_");
            object result = null;

            if (info.TargetMethod.Name == "add_PropertyChanged")
            {
                var propertyChangedEventHandler = info.Arguments[0] as PropertyChangedEventHandler;
                this._changed += propertyChangedEventHandler;
            }
            else if (info.TargetMethod.Name == "remove_PropertyChanged")
            {
                var propertyChangedEventHandler = info.Arguments[0] as PropertyChangedEventHandler;
                this._changed -= propertyChangedEventHandler;
            }
            else
            {
                result = info.TargetMethod.Invoke(this.Proxy, info.Arguments);
            }

            if (isSetter)
            {
                var propertyName = info.TargetMethod.Name.Substring("set_".Length);
                this._changed(info.Target, new PropertyChangedEventArgs(propertyName));
            }

            return result;
        }

        #endregion
    }

}