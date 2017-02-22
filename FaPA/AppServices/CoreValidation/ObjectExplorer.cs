using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using FaPA.Core;
using FaPA.Data;
using FaPA.DomainServices.Utils;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.AppServices.CoreValidation
{
    public static class ObjectExplorer
    {
        private static BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;

        public static List<T> FindAllInstancesDeep<T>(object value) where T : class
        {
            var exploredObjects = new HashSet<object>();
            var found = new List<T>();

            FindAllInstancesDeep(value, exploredObjects, found);

            return found;
        }

        private static void FindAllInstancesDeep<T>( object value, HashSet<object> exploredObjects, List<T> found ) where T : class
        {
            if ( value == null || exploredObjects.Contains( value ) || value.GetType().IsEnum ) return;

            exploredObjects.Add( value );

            var enumerable = value as IEnumerable;

            if ( enumerable != null )
            {
                foreach ( var item in enumerable )
                    FindAllInstancesDeep<T>( item, exploredObjects, found );
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

                    FindAllInstancesDeep<T>( propertyValue, exploredObjects, found );
                }
            }
        }

        public static object DeepProxiedCopyOfType<T>(object value) where T : class
        {
            var exploredObjects = new HashSet<object>();
            var nameSpac = typeof ( T ).Namespace;
            var copy = value.Copy();
            return DeepProxiedCopyOfType<T>( copy, exploredObjects, nameSpac );

        }

        private static object DeepProxiedCopyOfType<T>(object value, HashSet<object> exploredObjects, string nameSpace) where T : class
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
                        if (app.GetType().IsEnum) continue;
                        var newVal = DeepProxiedCopyOfType<T>(app, exploredObjects, nameSpace);
                        if (newVal != null)
                        {
                            array.SetValue(newVal, i);
                        }
                    }
                }
                else if (IsList(enumerable))
                {
                    throw new NotImplementedException();
                }

                #endregion
            }
            else
            {
                var possibleMatch = value as T;
                var type = value.GetType();

                object proxyValue = null;
                if (possibleMatch != null)
                {
                    proxyValue = AddPropChangedAndDataErrorInterceptorProxyFactory.Create(type, value);
                }

                var properties = type.GetProperties(_bindingFlags).
                    Where(t => string.IsNullOrWhiteSpace(nameSpace) || t.PropertyType.FullName.StartsWith(nameSpace));

                foreach (var property in properties.Where(t => !t.PropertyType.IsEnum))
                {
                    var propertyValue = property.GetValue(value);

                    if (propertyValue == null)
                        continue;

                    var newPropValue = DeepProxiedCopyOfType<T>(propertyValue, exploredObjects, nameSpace);

                    if ( newPropValue != null )
                    {
                        ((BaseEntity)value).IsNotyfing = false;
                        ((BaseEntity)value).IsValidating = false;
                        property.SetValue(proxyValue, newPropValue);
                        ((BaseEntity)value).IsNotyfing = true;
                        ((BaseEntity)value).IsValidating = true;

                    }
                }

                return proxyValue;
            }

            return null;
        }

        public static object UnProxiedDeepCopy(object value ) 
        {
            var exploredObjects = new HashSet<object>();
            var copy = value.Copy();
            return UnProxiedDeepCopy( copy, exploredObjects);
        }

        private static object UnProxiedDeepCopy( object value, HashSet<object> exploredObjects)
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
                        var proxy = UnProxiedDeepCopy(app, exploredObjects);
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

                    var unproxied = UnProxiedDeepCopy( propertyValue, exploredObjects);

                    if (unproxied == null) continue;

                    var baseEntity = value as BaseEntity;
                    if ( baseEntity != null )
                    {
                        baseEntity.IsNotyfing = false;
                        baseEntity.IsValidating = false;
                    }

                    //((BaseEntity)value).IsNotyfing = false;
                    ((BaseEntity)value).IsValidating = false;
                    property.SetValue(value, unproxied);
                    ((BaseEntity)value).IsNotyfing = true;
                    //((BaseEntity)value).IsValidating = true;
                    
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

        private static void OverridesAllInstances( Type classType, Type rootType, HashSet<Type> exploredObjects, 
            XmlAttributeOverrides overrides, string mn ) 
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
            //Console.WriteLine(rootClassType.Name + " " + memberName + " " + proxyType.Name + " " + classType.Name);
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
