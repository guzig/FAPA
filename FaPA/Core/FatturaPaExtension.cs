using FaPA.AppServices.CoreValidation;
using FaPA.Core.FaPa;

namespace FaPA.Core
{
    public static class FatturaPaExtension
    {
        public static FatturaElettronicaType CopyDeep( this FatturaElettronicaType toCopy )
        {
            if ( toCopy == null )
                return null;
            return ( FatturaElettronicaType ) ObjectExplorer.UnProxiedDeepCopy( toCopy ); 
        }
    }
}