using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using FaPA.Core;
using FaPA.Data;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.AppServices.CoreValidation
{
    public static class ObjectExplorer
    {
        private static BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;

        public static List<T> FindAllInstances<T>(object value) where T : class
        {
            var exploredObjects = new HashSet<object>();
            var found = new List<T>();

            FindAllInstances(value, exploredObjects, found);

            return found;
        }

        private static void FindAllInstances<T>( object value, HashSet<object> exploredObjects, List<T> found ) where T : class
        {
            if ( value == null || exploredObjects.Contains( value ) || value.GetType().IsEnum ) return;

            exploredObjects.Add( value );

            var enumerable = value as IEnumerable;

            if ( enumerable != null )
            {
                foreach ( var item in enumerable )
                    FindAllInstances<T>( item, exploredObjects, found );
            }
            else
            {
                var possibleMatch = value as T;

                if ( possibleMatch != null )
                {
                    found.Add( possibleMatch );
                }

                var type = value.GetType();

                var properties = type.GetProperties( _bindingFlags );

                foreach ( var property in properties.Where( t => !t.PropertyType.IsEnum && 
                              t.PropertyType.FullName.StartsWith( "FaPA.Core" ) ) )
                {
                    var propertyValue = property.GetValue( value, null );

                    FindAllInstances<T>( propertyValue, exploredObjects, found );
                }
            }
        }

        public static void TryProxiedAllInstances<T>( ref object value, string nameSpace=null) where T : class
        {
            var exploredObjects = new HashSet<object>();

            TryProxiedAllInstances<T>( ref value, exploredObjects, nameSpace);
        }

        private static bool TryProxiedAllInstances<T>( ref object value, HashSet<object> exploredObjects, string nameSpace)  where T : class
        {
            bool isValueSet = false;

            if ( value == null || exploredObjects.Contains(value) || value.GetType().IsEnum ) return false;

            exploredObjects.Add(value);

            if (value is string) return false;
            var enumerable = value as IEnumerable;
            if ( enumerable != null)
            {
                #region IEnumerable loop

                var array = enumerable as Array;
                if ( array != null )
                {
                    for ( var i = 0; i < array.Length; i++ )
                    {
                        var app = array.GetValue( i );
                        if ( app.GetType().IsEnum ) continue;
                        if ( TryProxiedAllInstances<T>( ref app, exploredObjects, nameSpace ) )
                        {
                            array.SetValue( app, i );
                            isValueSet = true;
                        }
                    }
                    value = array;
                }
                else if ( IsList( enumerable ) )
                {
                    throw new NotImplementedException();
                }

                #endregion
            }
            else
            {
                var possibleMatch = value as T;
                var type = value.GetType();

                if (possibleMatch != null)
                {
                    var proxy = AddPropChangedAndDataErrorInterceptorProxyFactory.Create( type, value);
                    if (proxy != null)
                    {
                        value = proxy;
                    }
                    isValueSet = true;
                }
                
                var properties = type.GetProperties(_bindingFlags).
                    Where(t=> string.IsNullOrWhiteSpace(nameSpace) || t.PropertyType.FullName.StartsWith(nameSpace));

                foreach (var property in properties.Where( t=> !t.PropertyType.IsEnum ) )
                {
                    var propertyValue = property.GetValue(value);

                    if (propertyValue == null)
                        continue;

                    if ( TryProxiedAllInstances<T>( ref propertyValue, exploredObjects, nameSpace)  )
                        property.SetValue(value, propertyValue );
                }
            }

            return isValueSet;
        }

        public static object UnProxiedDeep(object value ) 
        {
            var exploredObjects = new HashSet<object>();

            return UnProxiedDeep( value, exploredObjects);

        }

        private static object UnProxiedDeep( object value, HashSet<object> exploredObjects)
        {
            if (value == null || exploredObjects.Contains(value) || value.GetType().IsEnum) return null;

            exploredObjects.Add(value);

            if (value is string) return null;
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                #region IEnumerable loop

                var array = enumerable as Array;
                if (array != null)
                {
                    for (var i = 0; i < array.Length; i++)
                    {
                        var app = array.GetValue(i);
                        if (app == null || app.GetType().IsEnum) continue;
                        var proxy = UnProxiedDeep(app, exploredObjects);
                        if (proxy == null) continue;

                        var baseEntity = proxy as BaseEntity;
                        if (baseEntity != null)
                        {
                            baseEntity.IsNotyfing = false;
                            baseEntity.IsValidating = false;
                        }

                        array.SetValue(proxy, i);

                        if (baseEntity != null)
                        {
                            baseEntity.IsNotyfing = true;
                            baseEntity.IsValidating = true;
                        }
                    }
                    return array;
                }
                else if ( IsList( enumerable ) )
                {
                    throw new NotImplementedException();
                }

                #endregion
            }
            else
            {
                var possibleMatch = value as IProxy;
                var type = value.GetType();
                var properties = type.GetProperties(_bindingFlags).ToArray();

                foreach (var property in properties )
                {
                    var propertyValue = property.GetValue(value);

                    if ( propertyValue == null || ( property.PropertyType.Namespace != null && 
                        !property.PropertyType.Namespace.StartsWith( "FaPA.Core" ) ) )
                    {
                        continue;
                    }

                    var unproxied = UnProxiedDeep( propertyValue, exploredObjects);

                    if (unproxied == null) continue;

                    var baseEntity = value as BaseEntity;
                    if ( baseEntity != null )
                    {
                        baseEntity.IsNotyfing = false;
                        baseEntity.IsValidating = false;
                    }

                    property.SetValue( value, unproxied );

                    if (baseEntity != null)
                    {
                        baseEntity.IsNotyfing = true;
                        baseEntity.IsValidating = true;
                    }
                }

                if (possibleMatch == null) return value;
                var inst = possibleMatch.Interceptor as PropChangedAndDataErrorDynProxyInterceptor;
                if ( inst == null ) return value;
                var proxy = inst.Proxy;
                return proxy ?? value;
            }

            return null;
        }


        public static void OverridesAllInstances( Type classType, XmlAttributeOverrides overrides ) 
        {
            var exploredObjects = new HashSet<Type>();

            OverridesAllInstances( classType, null, exploredObjects, overrides, null );

        }

        private static void OverridesAllInstances( Type classType, Type rootType, HashSet<Type> exploredObjects, XmlAttributeOverrides overrides, string mn ) 
        {
            const string targetNameSpace = "FaPA.Core.FaPa";

            if ( classType == null || exploredObjects.Contains( classType ) || classType.IsEnum ) return;

            exploredObjects.Add( classType );

            if (  typeof( IEnumerable ).IsAssignableFrom( classType ) )
            {
                var elementType = classType.GetElementType();

                if ( elementType.IsEnum ) return;

                GetAttrOverride( overrides, elementType, rootType,  mn );
                
                OverridesAllInstances( elementType, classType, exploredObjects, overrides, mn );
            }
            else
            {
                if ( classType.Namespace != null && classType.IsClass && classType.Namespace.StartsWith( targetNameSpace ) )
                {
                    //override root  
                    if ( !string.IsNullOrWhiteSpace( mn ) )
                    {
                        var xmlAttributes = overrides[classType];
                        if ( xmlAttributes == null )
                        {
                            GetAttrOverride( overrides, classType, rootType, mn );
                        }
                    }
                }

                var nestedTypes = (from n in classType.GetProperties()
                                   let type = n.PropertyType
                                   where type.Namespace != null && type.IsClass && type.Namespace.StartsWith( targetNameSpace )
                                   select n).ToArray();

                foreach ( var prop in nestedTypes.Where( t => !t.PropertyType.IsEnum ) )
                {
                    //explore current type recursion
                    OverridesAllInstances( prop.PropertyType, classType, exploredObjects, overrides, prop.Name );
                }

            }
        }

        private static void GetAttrOverride( XmlAttributeOverrides overrides, Type classType, Type rootClassType, string memberName )
        {
            var instance = Activator.CreateInstance( classType );
            var proxiedInstance = AddPropChangedAndDataErrorInterceptorProxyFactory.Create( classType, instance );
            var proxyType = proxiedInstance.GetType();
            var attribs = new XmlAttributes();
            attribs.XmlElements.Add( new XmlElementAttribute( proxyType ) );
            attribs.XmlElements.Add( new XmlElementAttribute( classType ) );
            overrides.Add( rootClassType, memberName, attribs );
        }

        private static bool IsList( object o )
        {
            if ( o == null ) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom( typeof( List<> ) );
        }
    }
}
