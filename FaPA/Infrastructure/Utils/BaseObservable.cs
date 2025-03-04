using System;
using System.ComponentModel;

namespace FaPA.Infrastructure.Helpers
{
    public abstract class BaseObservable: INotifyPropertyChanged
    {
        public abstract object Value { get; set; }

        public abstract void RaisePropertyChangedEvent();

        public abstract void NotifyChangedAlsoTo(BaseObservable @base);

        public abstract void Remove(BaseObservable @base);
        
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public abstract bool As<T>(Func<T, bool> arg);

        public abstract T As<T>();
    }
}