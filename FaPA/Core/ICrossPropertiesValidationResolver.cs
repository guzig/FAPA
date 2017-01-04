using System;
using System.Collections.Generic;


namespace FaPA.Core
{
    public interface ICrossPropertiesValidationResolver
    {
        void AddCrossCoupledPropValidation<TEntity>( ICrossPropertiesValidationContext<TEntity> crossPropContext );
        HashSet<string> TryGetCrossCoupledPropValidation( Type type, string propName );
    }
}
