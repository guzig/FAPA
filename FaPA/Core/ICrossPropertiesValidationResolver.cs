using System;
using System.Collections.Generic;


namespace FaPA.Core
{
    public interface ICrossPropertiesValidationResolver
    {
        void AddCrossCoupledPropValidationContext<TEntity>( ICrossPropertiesValidationContext<TEntity> crossPropContext );
        HashSet<string> TryGetCrossCoupledPropValidation( Type type, string propName );
    }
}
