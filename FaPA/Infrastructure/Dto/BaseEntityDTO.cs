using System;
using System.ComponentModel;
using Caliburn.Micro;
using FaPA.Core;

namespace FaPA.Infrastructure.Dto
{
    public abstract class  BaseEntityDto : PropertyChangedBase, IEntity, IDisposable
    {

        // This implementation are supposing your are using "POID uniqueness across all classes" strategy.
        private int? RequestedHashCode { get; set; }
        private long _id;
        public DomainResult DomainResult { get; set; }
        
        public event EventHandler<ValidationEventArgs> NotifyOfDataErrorInfo;

        protected void OnDataErrorInfoChanged(ValidationEventArgs args)
        {
            var handler = NotifyOfDataErrorInfo;
            if (handler != null)
            {
               handler(this,args);
            }
        }
        
        #region IEntity Members

        public virtual long Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        public virtual bool Equals(IEntity other)
        {
            if (null == other)
                return false;

            if (other.Id == 0L && Id == 0L)
                return other.GetHashCode().Equals(GetHashCode());

            return ReferenceEquals(this, other) || other.Id.Equals(Id);
        }

        #endregion

        public virtual int Version { get; set; }

        public virtual void OnChanged( PropertyChangedEventHandler onPropChanged )
        {
            this.PropertyChanged -= onPropChanged;
            this.PropertyChanged += onPropChanged;
        }

        public virtual void OnDataErrorInfo( EventHandler<ValidationEventArgs> onDataErrorInfo )
        {
            this.NotifyOfDataErrorInfo -= onDataErrorInfo;
            this.NotifyOfDataErrorInfo += onDataErrorInfo;
        }

        public abstract bool IsProxy();

        protected virtual bool IsTransient()
        {
            return Equals(Id, 0L);
        }
        
        public override bool Equals(object obj)
        {
            var that = obj as IEntity;
            return Equals(that);
        }

        public override int GetHashCode()
        {
            if (!RequestedHashCode.HasValue)
                RequestedHashCode = IsTransient() ? base.GetHashCode() : Id.GetHashCode();
            return RequestedHashCode.Value;
        }

        public virtual int CompareTo( IEntity other )
        {
            if ( other == null ) return 1;

            return other.Id.CompareTo( Id );
        }

        public virtual int CompareTo( object obj )
        {
            if ( obj == null ) return 1;

            var entity = obj as BaseEntity;

            if ( entity != null )
                return CompareTo( entity );

            throw new ArgumentException( "Object is not a BaseEntity" );
        }

        //public abstract string this[string columnName] { get; }
        
        //public abstract string Error { get; }

        public void Dispose()
        {
            NotifyOfDataErrorInfo = null;
        }



    }
}