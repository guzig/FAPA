using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FaPA.Core
{
    public interface ICrossPropertiesValidationContext<TEntity>
    {
        Type ContextType { get; }
        ICrossPropertiesValidationContext<TEntity> AddCrossCoupledPropValidation<TProp>( Expression<Func<TEntity, TProp>> property );
        HashSet<string> CrossPropertiesContext { get;  }
    }
}