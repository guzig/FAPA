using System.Collections.Generic;
using System.Linq;
using FaPA.Core;
using FaPA.DomainServices.Utils;

namespace FaPA.AppServices
{
    public static class SharedReferenceDataFactory
    {
        private static List<Comune> _comuni;

        public static List<Comune> Comuni
        {
            get
            {
                if ( _comuni != null ) return _comuni;
                _comuni = new ReferenceDataFactory().GetReferenceCollection<Comune>().OrderBy( p => p.Denominazione ).ToList();
                return _comuni;
            }

        }

        private static IList<Comune> _provincie;
        public static IList<Comune> Provincie
        {
            get
            {
                if ( _provincie != null ) return _provincie;

                _provincie = Comuni.DistinctBy( c => c.DenominazioneProvincia ).OrderBy( c => c ).ToList();
                return _provincie;
            }
        }
    }
}
