using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace FaPA.GUI.Controls.MyTabControl
{

    public class ListViewViewModel : WorkspaceViewModel
    {
        #region Fields
        
        #endregion // Fields

        #region Constructor

        public ListViewViewModel()
        {
            IsCloseable = false;
        }
       
        #endregion // Constructor

        #region Public Interface  

        private ICollectionView _userEntitiesView;
        public ICollectionView UserEntitiesView
        {
            get { return _userEntitiesView; }
            set
            {
                _userEntitiesView = value;
                NotifyOfPropertyChange(() => UserEntitiesView);

                if (value==null) return;

                //eventi esposti
                _userEntitiesView.CurrentChanged -= OnCurrentChanged;
                _userEntitiesView.CurrentChanged += OnCurrentChanged;

                _userEntitiesView.CurrentChanging -= OnCurrentChanging;
                _userEntitiesView.CurrentChanging += OnCurrentChanging;

                RequestClose += (o, e) =>
                {
                    _userEntitiesView.CurrentChanged -= OnCurrentChanged;
                    _userEntitiesView.CurrentChanging -= OnCurrentChanging;
                };

            }
        }
        
        public event EventHandler CurrentChanged;

        public event EventHandler CurrentChanging;
        
        #endregion // Public Interface

        private void OnCurrentChanged(object sender, EventArgs eventArgs)
        {
            var dispatcher = Dispatcher.CurrentDispatcher;

            dispatcher.BeginInvoke(new Action(() => {
                var handler = CurrentChanged;
                handler?.Invoke(UserEntitiesView.CurrentItem, eventArgs);
            }));
        }

        private void OnCurrentChanging(object sender, EventArgs eventArgs)
        {
            var dispatcher = Dispatcher.CurrentDispatcher;

            var currentItem = UserEntitiesView.CurrentItem;

            dispatcher.BeginInvoke(new Action(() =>
            {
                var handler = CurrentChanging;
                handler?.Invoke(currentItem, eventArgs);
            }));
        }

        public bool NeedRefresh { get; set; }

        //public void Refresh(BaseEntityDto entity)
        //{
        //    //UserCollection[UserEntitiesView.CurrentPosition] = entity;
        //    UserEntitiesView.Refresh();            
        //}

        #region  Base Class Overrides

        /*
        protected override void OnDispose()
        {
            foreach (FornitoreViewModel f in this.FornitoriVM)
                f.Dispose();

            this.FornitoriVM.Clear();
            this.FornitoriVM.CollectionChanged -= this.OnCollectionChanged;

            //_customerRepository.CustomerAdded -= this.OnCustomerAddedToRepository;
        }


        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (FornitoreViewModel f in e.NewItems)
                    f.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (FornitoreViewModel f in e.OldItems)
                    f.PropertyChanged -= this.OnCustomerViewModelPropertyChanged;
        }
        
       
        */

        #endregion 

    }
}
