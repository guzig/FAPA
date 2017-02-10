using System.Collections.Generic;
using System.Linq;
using FaPA.DomainServices.Utils;

namespace FaPA.AppServices.CoreValidation
{
    /// <summary>
    ///note:use of ValidateInstance.By because : "NHV was designed more thinking about properties-constraints than Business-Rules"
    ///see:http://fabiomaulo.blogspot.it/2010/01/nhibernatevalidator-changing-validation.html
    /// </summary>
    public abstract class NhInstanceValidator : NhValidator
    {
        private readonly IEnumerable<string> _itemLevelValidationGroup = new List<string>();

        protected virtual IEnumerable<string> ItemLevelValidationGroup
        {
            get { return _itemLevelValidationGroup; }
        }

        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( string columnName, object instance )
        {
            if ( Validator == null ) return new Dictionary<string, IEnumerable<string>>();

            if ( ItemLevelValidationGroup.Contains( columnName ) )
                return GetValidationErrors( instance );

            var errors = Validator.ValidatePropertyValue( instance, columnName ).
                DistinctBy( d => d.Message ).Select( d => d.Message ).ToList();

            return errors.Any() ? new Dictionary<string, IEnumerable<string>> { { columnName, errors } } :
                new Dictionary<string, IEnumerable<string>>();
        }
    }
}