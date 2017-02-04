using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Helpers;
using NHibernate.Criterion;

namespace FaPA.GUI.Feautures.Anagrafica
{
    public class Presenter: BaseCrudPresenter<Core.Anagrafica, View>
    {
        private FinderConfirmSearchEventArgs _dataFiltered;
        private Action<Presenter> _onLoaded;

        protected override QueryOver QueryCriteria
        {
            get { return QueryOver.Of<Core.Anagrafica>(); }
        }

        public GenericsObservable<bool> AllowEditing { get; set; }

        private string _onGridEmptyText = "Inserisci una nuova anagrafica";
        public string OnGridEmptyText
        {
            get { return _onGridEmptyText; }
            set { _onGridEmptyText = value; }
        }

        public void Initialize( Action<Presenter> onLoaded )
        {
            _onLoaded = onLoaded;
        }

        public void OnLoaded()
        {
            _onLoaded( this );
        }

        //ctor
        public Presenter()
        {
            View.Presenter = this;

            //var _comuni = new ReferenceDataFactory().GetReferenceCollection<Comune>().
            //    DistinctBy(c => c.Denominazione).ToDictionary(k => k.Denominazione, v => v);


        }

        public override void CreateNewModel(DetachedCriteria queryByExample)
        {
            var entities = GetExeCriteria<Core.Anagrafica>(queryByExample);
            SetUpModel(entities);
        }

        private void CreateNewModel(IList searchResult)
        {

            var entitiesdto = new ObservableCollection<Core.Anagrafica>( 
                searchResult.Cast<Core.Anagrafica>().ToList() );

            if (IsIncrementalSearch())
            {
                foreach (var userEntity in Model.UserEntities.Cast<Core.Anagrafica>()
                    .Where(userEntity => !entitiesdto.Contains(userEntity)))
                {
                    entitiesdto.Add(userEntity);
                }

            }
            Model.UserEntities = entitiesdto;
            Model.UserCollectionView = CollectionViewSource.GetDefaultView(Model.UserEntities);
            
            //var viewModel = Model.EditViewModel as IEditViewModel;
            //viewModel.SetUpCollectionView(Model.UserEntities, Model.UserCollectionView);
        }

        public override void CreateNewModel(int activeTab)
        {
            IsBusy = true;
            var entities = GetExeCriteria<Core.Anagrafica>(QueryCriteria);
            var entitiesdto = entities == null ? new ObservableCollection<Core.Anagrafica>()
                : new ObservableCollection<Core.Anagrafica>(entities);
            SetUpNewModel(0, entitiesdto);
            IsBusy = false;
        }

        public override void CreateNewModel(QueryOver queryByExample)
        {
            IsBusy = true;
            Model = new Model();
            InitViewModel(queryByExample);
            IsBusy = false;
        }

        protected override BaseCrudModel CreateNewModel()
        {
            return new Model();
        }

        #region search command stuff

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                if (_search != null) return _search;
                _search = new RelayCommand(param => OnSearch(), param =>
                {
                    var editLiquidazioneViewModel = Model.EditViewModel as EditAnagraficaViewModel;
                    return editLiquidazioneViewModel != null && editLiquidazioneViewModel.AllowFastSearch;
                });
                return _search;
            }

        }

        public void OnSearch()
        {
            const string presenterName = "SearchAnagrafica";
            var presenter = Presenters.CreateInstance(presenterName, null) as SearchAnagrafica.Presenter;
            if (presenter == null) return;
            presenter.ConfirmResult += OnConfirmResult;
            presenter.ShowDialog();
        }

        private void OnConfirmResult(object sender, FinderConfirmSearchEventArgs data)
        {
            _dataFiltered = data;
            if (_dataFiltered.DetachedCriteria != null)
            {
                CreateNewModel(_dataFiltered.DetachedCriteria);
            }
            else
            {
                if (_dataFiltered.Collection.Count > 0)
                    CreateNewModel(_dataFiltered.Collection);
            }
            Session.Clear();
        }

        #endregion
        
        #region INotifyDataSourceHit Members

        public override void QueryInProgress(bool inProgress)
        {
            //Model.EditViewModel. = inProgress;
        }

        #endregion

        private ICommand _entityChoosenCommand;
        public ICommand EntityChoosenCommand
        {
            get
            {
                if (_entityChoosenCommand != null)
                    return _entityChoosenCommand;
                _entityChoosenCommand = new RelayCommand(e => SetActiveWorkspace(
                   Model.Workspaces.First(w => w is EditAnagraficaViewModel)), o => true);
                return _entityChoosenCommand;
            }
        }
    }
}
