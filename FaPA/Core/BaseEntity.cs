using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FaPA.AppServices.CoreValidation;

namespace FaPA.Core
{
    public abstract class BaseEntity : IEntity, IValidatable 
    {
        
        #region Validation

        public virtual DomainResult DomainResult { get; protected set; } = new DomainResult( false );

        /// <summary>
        /// UI refresh happen in interceptor proxy
        /// see PropChangedAndDataErrorDynProxyInterceptor
        /// </summary>
        public virtual void HandleValidationResults()
        {
        }

        public virtual void HandleValidationResults( string propName )
        {
        }

        public abstract DomainResult Validate();

        public abstract DomainResult ValidatePropertyValue( string prop );

        protected void GetPersistentErrors( IDictionary<string, IEnumerable<string>> errors, string propName )
        {
            //get persistent layer validation
            var err = CoreValidatorService.GetValidationErrors(propName, this);
            var validationErrors = err == null ? null : CoreValidatorService.GetValidationErrors( propName, this ).ToArray();
            if ( validationErrors == null || !validationErrors.Any() ) return;
            foreach (var keyValuePair in validationErrors)
                errors.Add(keyValuePair.Key, keyValuePair.Value);
        }

        protected void GetPersistentErrors(IDictionary<string, IEnumerable<string>> errors)
        {
            //get persistent layer validation
            var validationErrors = CoreValidatorService.GetValidationErrors(this);

            if (validationErrors == null) return;

            foreach (var item in validationErrors)
                errors.Add(item.Key, item.Value);
        }

        public virtual bool IsValidating { get; set; } = false;

        public virtual bool IsNotyfing { get; set; } = true;

        #endregion


        #region IEntity Members

        // This implementation are supposing your are using "POID uniqueness across all classes" strategy.

        public virtual long Id { get; set; }

        public virtual bool Equals( IEntity other )
        {
            if ( null == other )
                return false;

            if ( other.Id == 0L && Id == 0L )
                return other.GetHashCode().Equals( GetHashCode() );

            return ReferenceEquals( this, other ) || other.Id.Equals( Id );
        }

        #endregion

        public virtual BaseEntity ShallowCopy()
        {
            return ( BaseEntity ) this.MemberwiseClone();
        }

        public virtual int Version { get; set; }

        protected virtual bool IsTransient()
        {
            return Equals( Id, 0L );
        }

        public virtual int CompareTo( IEntity other )
        {
            if ( other == null ) return 1;

            return other.Id.CompareTo( Id );
        }

        public override bool Equals( object obj )
        {
            var that = obj as IEntity;
            return Equals( that );
        }

        private int? _requestedHashCode;
        
        public override int GetHashCode()
        {
            if ( _requestedHashCode.HasValue ) return _requestedHashCode.Value;
            _requestedHashCode = IsTransient() ? base.GetHashCode() : Id.GetHashCode();
            return _requestedHashCode.Value;
        }


        public virtual int CompareTo( object obj )
        {
            if ( obj == null ) return 1;

            var entity = obj as BaseEntity;

            if ( entity != null )
                return this.CompareTo( entity );

            throw new ArgumentException( "Object is not a BaseEntity" );
        }

        public virtual PropertyChangedEventHandler PropertyChangedEventHandler => null;

    }
}
