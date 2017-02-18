using System;
using System.Collections.Generic;
using System.Reflection;
using FaPaTets.DbSetUp;
using FaPaTets.FatturaPa.FatturaPa_11;
using FaPA.Core;
using FaPA.Data;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Proxy.DynamicProxy;
using NUnit.Framework;

namespace FaPaTets.PersistanceTests
{
    [TestFixture]
    public class ProxyPersitanceTests : AbstractTestFixtureFixture
    {
        [Test]
        public void can_serialize_nested_proxies0()
        {
            var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ); //new AddPropertyChangedInterceptor()

            Anagrafica fattura;
            using ( var transaction = session.BeginTransaction() )
            {
                fattura = session.Get<Anagrafica>(32768L);
                transaction.Commit();
            }


            // Proxing
            var unproxy = fattura.Unproxy();

            UtilsPA.CheckAllTypesAreUnProxied<FaPA.Core.BaseEntity>( unproxy );

            object proxied = ObjectExtensions1.Copy( unproxy );



            //CheckAllTypesAreProxied<FaPA.Core.BaseEntity>( proxied );

            //var f = ObjectExtensions.Copy( proxied );

            //CheckAllTypesAreProxied<FaPA.Core.BaseEntity>( f );

        }
        [Test]
        public void Created_proxy_entity_should_be_persistable()
        {
            var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ); //new AddPropertyChangedInterceptor()

            var fattura = DataTestFactory.GetFattura();
            fattura.Init();

            fattura.SyncFatturaPa();

            UtilsPA.FillFatturaPa( fattura.FatturaPa );

            using ( var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCommittenteDB);
                session.SaveOrUpdate( typeof( Fattura ).FullName, fattura );
                session.Flush();
                transaction.Commit();
            }

            //var other = ( Fattura ) ObjectExplorer.UnProxiedDeepCopy( fattura );

            //var other = ( Fattura ) ObjectExtensions1.Copy( fattura );

            Fattura read;
            using (var tx = session.BeginTransaction())
            {
                session.Evict(session.Get<Fattura>(fattura.Id));
                read = session.Get<Fattura>(fattura.Id);
                tx.Commit();
            }

            Assert.IsInstanceOf<IProxy>(read);

            using ( var transaction = session.BeginTransaction() )
            {
                read.DatiGeneraliDocumento.Data = DateTime.Now.AddDays( 10 );
                read.TotaleFatturaDB = 101;
                session.Update( read );
                session.Flush();
                transaction.Commit();
            }


            Assert.AreEqual(fattura.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdCodice,
                            read.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdCodice);
        }

        [Test]
        public void Created_proxy_entity_should_handle_PropertyChanged()
        {
            var fattura = DataTestFactory.GetFattura();

            using (var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ) )
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCedenteDB);
                session.SaveOrUpdate(typeof(Anagrafica).FullName, fattura.AnagraficaCommittenteDB);
                session.SaveOrUpdate(typeof(Fattura).FullName, fattura);
                session.Flush();
                transaction.Commit();
            }

            Fattura fatturaInDb;
            using (var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor()))
            using (var transaction = session.BeginTransaction())
            {
                fatturaInDb = session.Get<Fattura>( fattura.Id );
                transaction.Commit();
            }


            Check_entity_handles_PropertyChanged(fatturaInDb);
        }

        [Test]
        public void Created_proxy_entity_should_handle_PropertyChanged1()
        {
            Fattura fatturaInDb;
            
            using ( var session = _sessionFactory.OpenSession( new AddPropertyChangedInterceptor() ) )
            using ( var transaction = session.BeginTransaction() )
            {
                session.FlushMode = FlushMode.Never;
                fatturaInDb = session.Get<Fattura>( 131072L );
                transaction.Commit();
            }
                
            Assert.IsInstanceOf<IProxy>( fatturaInDb.FatturaPa.FatturaElettronicaHeader );
            //Assert.That( eventWasCalled );
            //Assert.That( propertyName, Is.EqualTo( "Id" ) );
            //Assert.That( sender, Is.SameAs( fatturaInDb.FatturaPa ) );
        }

    }


    public static class ObjectExtensions1
    {
        private static readonly MethodInfo CloneMethod = typeof( Object ).GetMethod( "MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance );

        public static bool IsPrimitive( this Type type )
        {
            if ( type == typeof( String ) ) return true;
            return ( type.IsValueType & type.IsPrimitive );
        }

        public static Object Copy( this Object originalObject )
        {
            return InternalCopy( originalObject, new Dictionary<Object, Object>( new ReferenceEqualityComparer() ) );
        }
        private static Object InternalCopy( Object originalObject, IDictionary<Object, Object> visited )
        {
            if ( originalObject == null ) return null;
            var typeToReflect = originalObject.GetType();
            if ( IsPrimitive( typeToReflect ) ) return originalObject;
            if ( visited.ContainsKey( originalObject ) ) return visited[originalObject];
            if ( typeof( Delegate ).IsAssignableFrom( typeToReflect ) ) return null;
            var cloneObject = CloneMethod.Invoke( originalObject, null );
            if ( typeToReflect.IsArray )
            {
                var arrayType = typeToReflect.GetElementType();
                if ( IsPrimitive( arrayType ) == false )
                {
                    Array clonedArray = ( Array ) cloneObject;
                    clonedArray.ForEach( ( array, indices ) => array.SetValue( InternalCopy( clonedArray.GetValue( indices ), visited ), indices ) );
                }

            }
            visited.Add( originalObject, cloneObject );
            CopyFields( originalObject, visited, cloneObject, typeToReflect );
            RecursiveCopyBaseTypePrivateFields( originalObject, visited, cloneObject, typeToReflect );
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields( object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect )
        {
            if ( typeToReflect.BaseType != null )
            {
                RecursiveCopyBaseTypePrivateFields( originalObject, visited, cloneObject, typeToReflect.BaseType );
                CopyFields( originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate );
            }
        }

        private static void CopyFields( object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null )
        {
            foreach ( FieldInfo fieldInfo in typeToReflect.GetFields( bindingFlags ) )
            {
                if ( filter != null && filter( fieldInfo ) == false ) continue;
                if ( IsPrimitive( fieldInfo.FieldType ) ) continue;
                var originalFieldValue = fieldInfo.GetValue( originalObject );
                var clonedFieldValue = InternalCopy( originalFieldValue, visited );
                fieldInfo.SetValue( cloneObject, clonedFieldValue );
            }
        }
        public static T Copy<T>( this T original )
        {
            return ( T ) Copy( ( Object ) original );
        }
    }

    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        public override bool Equals( object x, object y )
        {
            return ReferenceEquals( x, y );
        }
        public override int GetHashCode( object obj )
        {
            if ( obj == null ) return 0;
            return obj.GetHashCode();
        }
    }

        public static class ArrayExtensions
        {
            public static void ForEach( this Array array, Action<Array, int[]> action )
            {
                if ( array.LongLength == 0 ) return;
                ArrayTraverse walker = new ArrayTraverse( array );
                do action( array, walker.Position );
                while ( walker.Step() );
            }
        }

        internal class ArrayTraverse
        {
            public int[] Position;
            private int[] maxLengths;

            public ArrayTraverse( Array array )
            {
                maxLengths = new int[array.Rank];
                for ( int i = 0; i < array.Rank; ++i )
                {
                    maxLengths[i] = array.GetLength( i ) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step()
            {
                for ( int i = 0; i < Position.Length; ++i )
                {
                    if ( Position[i] < maxLengths[i] )
                    {
                        Position[i]++;
                        for ( int j = 0; j < i; j++ )
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    
}