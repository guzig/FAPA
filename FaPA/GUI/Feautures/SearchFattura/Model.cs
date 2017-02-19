using System.Collections.ObjectModel;
using System.ComponentModel;
using Caliburn.Micro;
using FaPA.Infrastructure.Helpers;

namespace FaPA.GUI.Feautures.SearchFattura
{
    public class Model : PropertyChangedBase
    {
       public FattureFinder FattureFinder { get; set; }

       public Model()
        {
            AllowSearch = new Observable(true);

            FattureFinder = new FattureFinder(typeof(Core.Fattura), OnQueryCriteria);
            FattureFinder.IsValidCriteria.NotifyChangedAlsoTo(AllowSearch);

            ResultEntryCount = new GenericsObservable<int>(0);

            AllowConfirmResult = new Observable(false);
            IsBusy = new GenericsObservable<bool>(false);

            SelectedEntryCount = new Observable(0);
            SelectedEntryCount.NotifyChangedAlsoTo(AllowConfirmResult);
            
            AllowEditing = new Observable(true);
            AllowEditing.NotifyChangedAlsoTo(AllowSearch);
        }

        private ICollectionView _fatture;
        public ICollectionView Fatture
        {
            get { return _fatture; }
            set
            {
                _fatture = value;
                NotifyOfPropertyChange(() => Fatture);
            }
        }

        private ObservableCollection<Core.Fattura> _pagedCollection;

        public ObservableCollection<Core.Fattura> PagedCollection
        {
            get { return _pagedCollection; }
            set { _pagedCollection = value; NotifyOfPropertyChange(() => PagedCollection); }
        }

        public BaseObservable AllowSearch { get; set; }
        public BaseObservable AllowEditing { get; set; }
        public GenericsObservable<bool> IsBusy { get; set; }
        public BaseObservable AllowConfirmResult { get; set; }
        public BaseObservable SelectedEntryCount { get; set; }
        public GenericsObservable<int> ResultEntryCount { get; set; }

        private bool _dataSourceHit;
        public bool DataSourceHit
        {
            get { return _dataSourceHit; }
            set
            {
                _dataSourceHit = value; NotifyOfPropertyChange(() => DataSourceHit);
                IsBusy.Value = _dataSourceHit;
            }
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyOfPropertyChange(() => Count);
            }
        }
        
        private string _allowedGridProperties;
        private bool _isFetchCompleted;

        public string AllowedGridProperties
        {
            get { return _allowedGridProperties; }
            set
            {
                _allowedGridProperties = value;
                NotifyOfPropertyChange(() => AllowedGridProperties);
            }
        }

        public bool IsFetchCompleted
        {
            get { return _isFetchCompleted; }
            set
            {
                if (value.Equals(_isFetchCompleted)) return;
                _isFetchCompleted = value;
                NotifyOfPropertyChange(() => IsFetchCompleted);
            }
        }

        private void OnQueryCriteria(string associationPath)
        {
            if (string.IsNullOrWhiteSpace(AllowedGridProperties) || !AllowedGridProperties.Contains(associationPath))
            {
                AllowedGridProperties = AllowedGridProperties + associationPath;
            }

        }

    }
}
