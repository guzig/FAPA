using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FaPA.GUI.Utils;

namespace FaPA.Core
{
    public class CrossPropertiesValidationContext<TEntity> : ICrossPropertiesValidationContext<TEntity>
    {
        private readonly HashSet<string> _crossPropertiesContext = new HashSet<string>();
        
        public Type ContextType => typeof(TEntity);

        public ICrossPropertiesValidationContext<TEntity> AddCrossCoupledPropValidation<TProp>( Expression<Func<TEntity, TProp>> property )
        {
            _crossPropertiesContext.Add( ReflHelpers.GetPropertyName( property ) );
            return this;
        }

        public HashSet<string> CrossPropertiesContext => _crossPropertiesContext;
    }
}