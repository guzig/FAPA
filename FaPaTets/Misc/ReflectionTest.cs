using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using FaPA.Core;

namespace FaPaTets.Misc
{

    [TestFixture]
    public class ReflectionTest
    {
        /// <summary>
        /// Convert a lambda expression for a getter into a setter
        /// </summary>
        public static Action<T, TProperty> GetSetter<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var property = (PropertyInfo)memberExpression.Member;
            var setMethod = property.GetSetMethod();

            var parameterT = Expression.Parameter(typeof(T), "x");
            var parameterTProperty = Expression.Parameter(typeof(TProperty), "y");

            var newExpression = Expression.Lambda<Action<T, TProperty>>(
                    Expression.Call(parameterT, setMethod, parameterTProperty),
                    parameterT, parameterTProperty );

            return newExpression.Compile();
        }

        public TProperty MyMethod<T, TProperty>(T myObject, Func<T, TProperty> selector)
        {
            return selector(myObject);
        }

        [Test]
        public void SetterByFunc()
        {
            var fattura = new Fattura();
          
            var fornitore = new Fornitore();
            var f  = GetSetter1( (Fattura x) => x.AnagraficaCedenteDB);
            f(fattura,fornitore);

            Assert.AreEqual(fornitore, fattura.AnagraficaCedenteDB);

            var g = MyMethod<Fattura, string>(fattura, (Fattura x) => x.CigDB);

            fattura.AnagraficaCedenteDB = null;

            var t = GetSetter2((Fattura x) => x.AnagraficaCedenteDB);

            t.Item1(fattura, fornitore);

            var p = t.Item2(fattura);
            
            Assert.AreEqual(fornitore, fattura.AnagraficaCedenteDB);
        }

        Action<T, TProperty> GetSetter1<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return GetSetter(expression);
        }

        Tuple<Action<T, TProperty>, Func<T, TProperty>> GetSetter2<T, TProperty>(
            Expression<Func<T, TProperty>> expression)
        {
            var setterExp =  GetSetter(expression);
            var getter = expression.Compile();
            return new Tuple<Action<T, TProperty>, Func<T, TProperty>>(setterExp, getter);
        }
    }
}
