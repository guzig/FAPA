using System;
using System.ComponentModel;
using FaPA.Core;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.Data
{
    public class PropChangedAndDataErrorDynProxyInterceptor : NHibernate.Proxy.DynamicProxy.IInterceptor
    {
        private readonly PropertyChangedEventHandler _onChangedHanler;
        public object Proxy { get; set; }
        private PropertyChangedEventHandler _changed = delegate { };
        private event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        
        public PropChangedAndDataErrorDynProxyInterceptor(){}

        public PropChangedAndDataErrorDynProxyInterceptor(object proxy, PropertyChangedEventHandler onChangedHanler)
        {
            if ( onChangedHanler != null )
            {
                _onChangedHanler = onChangedHanler;
                _changed += _onChangedHanler;
            }

            Proxy = proxy;
        }

        public object Intercept(InvocationInfo info)
        {
            switch (info.TargetMethod.Name)
            {
                case "add_PropertyChanged":
                    _changed += info.Arguments[0] as PropertyChangedEventHandler;
                    return null;
                case "remove_PropertyChanged":

                    var propertyChangedEventHandler = info.Arguments[0] as PropertyChangedEventHandler;
                    _changed -= propertyChangedEventHandler;

                    if ( _onChangedHanler != null && propertyChangedEventHandler != null &&
                        _onChangedHanler.Equals( propertyChangedEventHandler ) )
                        _changed -= propertyChangedEventHandler;
                    return null;
                case "add_ErrorsChanged":
                    ErrorsChanged += info.Arguments[0] as EventHandler<DataErrorsChangedEventArgs>;
                    return null;
                case "remove_ErrorsChanged":
                    ErrorsChanged -= info.Arguments[0] as EventHandler<DataErrorsChangedEventArgs>;
                    return null;
                case "get_HasErrors":
                    var validator = info.Target as IValidatable;
                    if (validator?.DomainResult == null) return false;
                    return !validator.DomainResult.Success;
                case "GetErrors":
                    var validatr = info.Target as IValidatable;
                    if (validatr?.DomainResult == null) return null;
                    var propName = (string) info.Arguments[0];
                    return validatr.DomainResult.PropErrors(propName);
                case "HandleValidationResults":
                    RaiseErrors( info.Target );
                    return null;
            }

            var returnValue = info.TargetMethod.Invoke( Proxy, info.Arguments );

            if ( !info.TargetMethod.Name.StartsWith("set_") ) return returnValue;
            
            var propertyName = info.TargetMethod.Name.Substring("set_".Length);

            //Validation
            var entity = info.Target as BaseEntity;
            ValidatePropValue( propertyName, entity );

             if ( entity != null && propertyName != nameof(entity.IsNotyfing) && 
                  propertyName != nameof(entity.IsValidating) && entity.IsNotyfing)
             {
                 //INotifyPropertyChanged
                 _changed(info.Target, new PropertyChangedEventArgs(propertyName));
             }

            if ( entity != null && propertyName != nameof( entity.IsValidating ) && 
                 propertyName != nameof(entity.IsNotyfing) && entity.IsValidating )
            {
                //RaiseErrors(info.Target);
                ErrorsChanged?.Invoke( info.Target, new DataErrorsChangedEventArgs(propertyName));               
            }

            return returnValue;
        }

        private static void ValidatePropValue( string propertyName, BaseEntity entity )
        {
            if ( entity != null && entity.IsValidating )
            {
                var validtor = ( IValidatable ) entity;
                validtor?.ValidatePropertyValue( propertyName );
            }
        }

        private void RaiseErrors(object target)
        {
            var valdtor = target as IValidatable;
            if ( valdtor == null ) return;
            if ( valdtor.DomainResult.Success ) return ;
            foreach (var pair in valdtor.DomainResult.Errors)
            {
                ErrorsChanged?.Invoke( target, new DataErrorsChangedEventArgs( pair.Key ) );
            }
        }
    }
}