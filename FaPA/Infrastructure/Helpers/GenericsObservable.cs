using System.ComponentModel;

namespace FaPA.Infrastructure.Helpers
{
    public class GenericsObservable<T> : INotifyPropertyChanged
	{
		private T value;
        
		public GenericsObservable()
		{}

		public GenericsObservable(T value)
		{
			this.value = value;
		}

		public T Value
		{
			get{ return value;}
			set
			{
				this.value = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}

		public static implicit operator T(GenericsObservable<T> val)
		{
			return val.value;
		}
        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
	}
}