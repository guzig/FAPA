using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FaPA.Core;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.Data
{
    public class PropChangedAndDataErrorDynProxyInterceptor : NHibernate.Proxy.DynamicProxy.IInterceptor, ICrossPropertiesValidationResolver
    {
        private readonly PropertyChangedEventHandler _onChangedHanler;
        public object Proxy { get; set; }
        private PropertyChangedEventHandler _changed = delegate { };
        private readonly Type _proxyType;
        private event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly CrossPropertiesValidationResolver _crossPropsValidationResolver = 
            new CustomCrossPropertiesValidationResolver();

        public PropChangedAndDataErrorDynProxyInterceptor(){}

        public PropChangedAndDataErrorDynProxyInterceptor(object proxy, PropertyChangedEventHandler onChangedHanler)
        {
            if ( onChangedHanler != null )
            {
                _onChangedHanler = onChangedHanler;
                _changed += _onChangedHanler;
            }

            Proxy = proxy;
            _proxyType = Proxy.GetType();
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
            }

            if ( info.TargetMethod.Name == "HandleValidationResults" )
            {
                var intercptr = info.Target as IProxy;
                if ( intercptr?.Interceptor is PropChangedAndDataErrorDynProxyInterceptor && 
                    ReferenceEquals( this, intercptr.Interceptor ) )
                {
                    if ( info.Arguments != null && info.Arguments.Any() )
                    {
                        ShowPropValidationError( ( string ) info.Arguments[0], info.Arguments[1] );
                    }
                    else
                    {
                        RaiseErrors( info.Target );
                    }
                    return null;
                }
            }

            var returnValue = info.TargetMethod.Invoke( Proxy, info.Arguments );

            if ( !info.TargetMethod.Name.StartsWith("set_") ) return returnValue;
            
            var propertyName = info.TargetMethod.Name.Substring("set_".Length);
            var entity = info.Target as BaseEntity;

            //Validation
            if ( entity != null && propertyName != nameof( entity.IsValidating ) && 
                 propertyName != nameof(entity.IsNotyfing) && entity.IsValidating )
            {
                ValidatePropValue( propertyName, entity );
                ShowPropValidationError( propertyName, info.Target );
            }

            //INotifyPropertyChanged
             if ( entity != null && propertyName != nameof(entity.IsNotyfing) && 
                  propertyName != nameof(entity.IsValidating) && entity.IsNotyfing)
             {
                 _changed(info.Target, new PropertyChangedEventArgs(propertyName));
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
                ShowPropValidationError( pair.Key, target );
            }
        }

        private void ShowPropValidationError( string propertyName, object target )
        {
            var crossProperties = _crossPropsValidationResolver.TryGetCrossCoupledPropValidation( _proxyType, propertyName );
            if ( crossProperties == null)
            {
                ErrorsChanged?.Invoke( target, new DataErrorsChangedEventArgs( propertyName ) );
            }
            else
            {
                foreach ( var propName in crossProperties )
                {
                    ErrorsChanged?.Invoke( target, new DataErrorsChangedEventArgs( propName ) );
                }
            }
        }

        public virtual void AddCrossCoupledPropValidationContext<TEntity>( 
            ICrossPropertiesValidationContext<TEntity> crossPropContext )
        {
            _crossPropsValidationResolver.AddCrossCoupledPropValidationContext( crossPropContext );
        }

        public virtual HashSet<string> TryGetCrossCoupledPropValidation( Type type, string propName )
        {
            return _crossPropsValidationResolver.TryGetCrossCoupledPropValidation( type, propName );
        }
    }
}