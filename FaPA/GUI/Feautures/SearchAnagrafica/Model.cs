using System.Collections.ObjectModel;
using System.ComponentModel;
using Caliburn.Micro;
using FaPA.Infrastructure.Helpers;
using NHibernate.Criterion;

namespace FaPA.GUI.Feautures.SearchAnagrafica
{
    public class Model : PropertyChangedBase
    {
        public AnagraficaFinder AnagraficaFinder { get; private set; }

        public Model()
        {
            AllowSearch = new Observable(true);
            IsBusy = new GenericsObservable<bool>(false);

            ResultEntryCount = new GenericsObservable<int>(0);

            AllowEditing = new Observable(true);
            AllowEditing.NotifyChangedAlsoTo(AllowSearch);

            AllowConfirmResult = new Observable(false);
            SelectedItemsCount = new Observable(0);
            SelectedItemsCount.NotifyChangedAlsoTo(AllowConfirmResult);

            AnagraficaFinder = new AnagraficaFinder(typeof(Core.Anagrafica));

            Core.Anagrafica root=null;
            AnagraficaFinder.QueryCriteria = QueryOver.Of( () => root );

            AnagraficaFinder.IsValidCriteria.NotifyChangedAlsoTo(AllowSearch);
        }

        public BaseObservable AllowSearch { get; set; }
        public BaseObservable AllowEditing { get; set; }
        public GenericsObservable<bool> IsBusy { get; set; }
        public BaseObservable AllowConfirmResult { get; set; }
        public BaseObservable SelectedItemsCount { get; set; }
        public GenericsObservable<int> ResultEntryCount { get; set; }
        private string _allowedGridProperties;
        public string AllowedGridProperties
        {
            get { return _allowedGridProperties; }
            set
            {
                _allowedGridProperties = value;
                NotifyOfPropertyChange( () => AllowedGridProperties );
            }
        }

        private ObservableCollection<Core.Anagrafica> _anagrafiche;
        public ObservableCollection<Core.Anagrafica> Anagrafiche
        {
            get { return _anagrafiche; }
            set {
                _anagrafiche = value;
                NotifyOfPropertyChange( () => Anagrafiche );
            }
        }

        private ICollectionView _anagraficheView;
        public ICollectionView AnagraficheView
        {
            get { return _anagraficheView; }
            set
            {
                if (Equals(value, _anagraficheView)) return;
                _anagraficheView = value;
                NotifyOfPropertyChange(() => AnagraficheView);
            }
        }
   
    }
}
