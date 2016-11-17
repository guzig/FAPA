using System.ComponentModel;

namespace FaPA.Infrastructure.Finder
{
    public class ItemValue<T> : INotifyPropertyChanged//, IDataErrorInfo
    {
        private T _item;
        public T Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}