using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FaPA.Infrastructure.Helpers
{
    public class Observable : BaseObservable
    {
        public Observable()
        {

        }

        public Observable(object value)
        {
            _value = value;
        }

        private object _value;

        public override bool As<T>(Func<T, bool> arg)
        {
            return arg((T)_value);
        }
        
        public override T As<T>()
        {
            return (T)Value ;
        }
        private readonly List<BaseObservable> _children = new List<BaseObservable>();

        public override object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChangedEvent();
            }
        }

        public override void RaisePropertyChangedEvent()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Value"));

            foreach (var child in _children.Where(child => child!=null))
            {
                child.RaisePropertyChangedEvent();
            }
        }

        public override event PropertyChangedEventHandler PropertyChanged = delegate { };

        public override void NotifyChangedAlsoTo(BaseObservable observable)
        {
            if (observable == null)
                throw new ArgumentNullException("observable");

            _children.Add(observable);
        }

        public override void Remove(BaseObservable observable)
        {
            _children.Remove(observable);
        }

    }
}