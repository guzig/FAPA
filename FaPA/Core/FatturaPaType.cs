using System;
using System.Data;
using FaPA.AppServices.CoreValidation;
using FaPA.Core.FaPa;
using NHibernate;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace FaPA.Core
{
    public class FatturaPaType : IUserType
    {
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);

            if (obj == null) return null;

            var xmlContent = (string)obj;

            if (string.IsNullOrWhiteSpace( xmlContent ) )
                throw new Exception("Expected data to be fatturapa.");

            if ( owner is IProxy )
            {
                object toObject = SerializerHelpers.XmlToObject( xmlContent );
                return ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntityFpa>( toObject );
            }

            return SerializerHelpers.XmlToObject( xmlContent );
        }
        
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                var fatturaElettronica = (FatturaElettronicaType)value;
                var unProxiedDeep = ObjectExplorer.UnProxiedDeepCopy( fatturaElettronica );
                var xmlStream = SerializerHelpers.ObjectToXml( ( FatturaElettronicaType ) unProxiedDeep ); 
                ((IDataParameter)cmd.Parameters[index]).Value = xmlStream;
            }
        }

        public object DeepCopy(object value)
        {
            return value != null ? ObjectExplorer.UnProxiedDeepCopy( value ) : null;
        }

        public object Replace(object original, object target, object owner)
        {
            throw new NotImplementedException();
        }

        public object Assemble(object cached, object owner)
        {
            var str = cached as string;
            return str != null ? SerializerHelpers.XmlToObject(str) : null;
        }

        public object Disassemble(object value)
        {
            var val = value as FatturaElettronicaType;
            return val != null ? SerializerHelpers.ObjectToXml( val ) : null;
        }

        public SqlType[] SqlTypes => new SqlType[] { new FatturaPaType.SqlXmlStringType() };

        public Type ReturnedType
        {
            get { return typeof( FatturaElettronicaType ); }
        }

        public bool IsMutable
        {
            get { return true; }
        }

        private class SqlXmlStringType : SqlType
        {
            public SqlXmlStringType() : base(DbType.String, 4001) // anything over 4000 is nvarchar(max)
            { }
        }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;

            var xString = x as string;
            var yString = y as string;
            if (xString == null || yString == null) return false;

            return xString.Equals(yString);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }
    }
}



