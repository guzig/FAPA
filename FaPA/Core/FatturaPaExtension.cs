using FaPA.Core.FaPa;

namespace FaPA.Core
{
    public static class FatturaPaExtension
    {
        public static FatturaElettronicaType CopyDeep( this FatturaElettronicaType toCopy )
        {
            if ( toCopy == null )
                return null;
            var xmlStream = SerializerHelpers.ObjectToXml( ( FatturaElettronicaType ) toCopy );
            return SerializerHelpers.XmlToObject( xmlStream );
        }
    }
}