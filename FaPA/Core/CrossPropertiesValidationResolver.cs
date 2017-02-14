using System;
using System.Collections.Generic;
using System.Linq;

namespace FaPA.Core
{
    [Serializable]
    public  class CrossPropertiesValidationResolver: ICrossPropertiesValidationResolver
    {
        private readonly Dictionary<Type, HashSet<HashSet<string>>> _crossPropetiesContexts = 
            new Dictionary<Type, HashSet<HashSet<string>>>();

        public void AddCrossCoupledPropValidationContext<TEntity>( ICrossPropertiesValidationContext<TEntity> crossPropContext )
        {
            if ( _crossPropetiesContexts.ContainsKey( crossPropContext.ContextType ) )
            {
                _crossPropetiesContexts[crossPropContext.ContextType].Add( crossPropContext.CrossPropertiesContext );
            }
            else
            {
                _crossPropetiesContexts.Add( crossPropContext.ContextType, 
                    new HashSet<HashSet<string>>() {crossPropContext.CrossPropertiesContext}  );
            }
        }

        public HashSet<string> TryGetCrossCoupledPropValidation( Type type, string propName )
        {
            return !_crossPropetiesContexts.ContainsKey( type ) ? null : 
                _crossPropetiesContexts[type].FirstOrDefault( c => c.Contains( propName ) );
        }
    }
}