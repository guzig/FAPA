using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FaPA.Infrastructure.Utils;

namespace FaPA.Core
{
    public class CrossPropertiesValidationContext<TEntity> : ICrossPropertiesValidationContext<TEntity>
    {
        public Type ContextType => typeof(TEntity);

        public ICrossPropertiesValidationContext<TEntity> AddCrossCoupledPropValidation<TProp>( Expression<Func<TEntity, TProp>> property )
        {
            CrossPropertiesContext.Add( ReflHelpers.GetPropertyName( property ) );
            return this;
        }

        public HashSet<string> CrossPropertiesContext { get; } = new HashSet<string>();
    }
}