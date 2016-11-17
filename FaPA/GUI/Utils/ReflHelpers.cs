using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FaPA.GUI.Utils
{
    public static class ReflHelpers
    {
        public static string GetNestedPropertyName<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            return String.Join(".", GetMembersOnPath(expression.Body as MemberExpression).Select(OnSelector)
                                   .Reverse());

        }

        private static string OnSelector(MemberExpression m)
        {
            return m.Member.Name;
        }

        public static IEnumerable<MemberExpression> GetMembersOnPath(MemberExpression expression)
        {
            while (expression != null)
            {
                yield return expression;
                expression = expression.Expression as MemberExpression;
            }
        }

        public static string GetPropertyName<T, TResult>(Expression<Func<T, TResult>> propertyExpression)
        {
            string nestedProp = GetNestedPropertyName(propertyExpression);
            if (!string.IsNullOrWhiteSpace(nestedProp))
                return nestedProp;

            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            return propertyInfo.Name;
        }

        public static Type GetPropertyType<T, TProp>(Expression<Func<T, TProp>> expression)
        {
            MemberExpression ex = expression.Body as MemberExpression;

            if (ex == null)
                return expression.ReturnType;

            return ((PropertyInfo)ex.Member).PropertyType;

        }

        public static object GetPropertyValue(object instance, string properyName)
        {
            if (instance == null)
                throw new Exception("instance");

            return instance.GetType().GetProperty(properyName).GetValue(instance, null);
        }

        //public static string GetPropertyPath(object nodeProp, object nestedProp, Func<Type, bool> isMarked)
        //{
        //    if (nodeProp == null || nestedProp == null || ReferenceEquals(nodeProp, nestedProp))
        //        return null;

        //    var path = new StringBuilder();
        //    GetNestedPropPath(nodeProp, nestedProp, isMarked, path);
        //    //return the property that contains components
        //    return path.Length == 0 ? null : path.ToString().Split('.')[0];
        //}

        //private static void GetNestedPropPath(object nodeProp, object nestedProp, Func<Type, bool> isMarked, StringBuilder path)
        //{
        //    if (!isMarked(nodeProp.GetType()) || !isMarked(nestedProp.GetType())) return;

        //    var props = nodeProp.GetType().GetProperties();

        //    foreach (var property in props)
        //    {
        //        var actualProp = property.GetValue(nodeProp, null);

        //        if (actualProp == null || !isMarked(property.PropertyType)) continue;

        //        //ricorsione 

        //        bool isPath = false;
        //        if (!ReferenceEquals(actualProp, nestedProp))
        //            GetNestedPropPath(actualProp, nestedProp, isMarked, path);
        //        else
        //        {
        //            isPath = true;
        //        }

        //        if (!isPath && path.Length == 0) continue;

        //        if (path.Length > 0)
        //            path.Insert(0, ".").Insert(0, property.Name);
        //        else
        //            path.Insert(0, property.Name);

        //        break;
        //    }
        //}

        //public static object GetParentObject(object rootInstance, string property)
        //{
        //    var part = property.Split('.');
        //    if (part.Length == 1)
        //    {
        //        var p = rootInstance.GetType().GetProperty(part[0]);
        //        return p != null ? rootInstance : null;
        //    }
        //    var target = rootInstance;
        //    for (var index = 0; index < part.Length - 1; index++)
        //    {
        //        var prop = part[index];
        //        var p = target.GetType().GetProperty(prop);
        //        var val = p.GetValue(target, null);

        //        if (val == null) break;

        //        target = val;

        //    }
        //    return target;
        //}

        public static void SetPropertyValue(Type objectType, object instance, string properyName, object value, object[] index)
        {
            if (objectType == null)
                throw new ArgumentNullException("objectType");

            if (instance == null)
                throw new ArgumentNullException("instance");

            var propertyInfo = objectType.GetProperty(properyName);

            if (propertyInfo != null)
                propertyInfo.SetValue(instance, value, index);
        }

        public static void CopyPropertyValue(object source, object instance, string properyName)
        {
            var propValue = GetPropertyValue(source, properyName);
            SetPropertyValue(instance.GetType(), instance, properyName, propValue, null);
        }


        //public static string GetProperty<T, TValue>(Expression<Func<T, TValue>> selector)
        //{
        //    Expression body = selector;
        //    if (body is LambdaExpression)
        //    {
        //        body = ((LambdaExpression)body).Body;
        //    }
        //    switch (body.NodeType)
        //    {
        //        case ExpressionType.MemberAccess:
        //            return ((MemberExpression)body).Member.Name;
        //        default:
        //            throw new InvalidOperationException();
        //    }
        //}

        /*
        public static object CreateAndHydrate(Type baseType, Func<Type, bool> isMarked)
        {
            try
            {
                var rootInstance = Activator.CreateInstance(baseType);
                Hydrate(baseType, rootInstance, isMarked);
                return rootInstance;

            }
            catch (Exception)
            {
                return null;
            }

        }

        private static void Hydrate(Type baseType, object nestedInstance, Func<Type, bool> isMarked)
        {
            var props = baseType.GetProperties();

            foreach (var property in props)
            {
                if (!isMarked(property.PropertyType)) continue;
                object instance;
                try
                {
                    instance = Activator.CreateInstance(property.PropertyType);
                }
                catch (Exception)
                {
                    continue;
                }
                
                //assegno l'istanza del tipo innestato all'istanza root
                nestedInstance.GetType().InvokeMember(property.Name,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                Type.DefaultBinder, nestedInstance, new[] { instance });
                //ricorsione 
                Hydrate(property.PropertyType, instance, isMarked);

            }



        }
        */
    }
}