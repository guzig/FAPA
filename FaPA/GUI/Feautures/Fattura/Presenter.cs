using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FaPA.Core;
using FaPA.DomainServices.Utils;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Design.Events.Emule.GUI.Design.Events;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace FaPA.GUI.Feautures.Fattura
{
    public class Presenter : BaseCrudPresenter<Core.Fattura, View>
	{

        //ctor
        public Presenter()
        {          
            View.Presenter = this;
           
            EventPublisher.Register<RemoveTabView>( OnCancelEditOnViewModel );
        }
        
        #region props

        private IList<Fornitore> _fornitori;
        public IList<Fornitore> Fornitori
        {
            get { return _fornitori; }
            set
            {
                _fornitori = value;
                NotifyOfPropertyChange(() => Fornitori);
            }
        }

        private IList<Committente> _committenti;
        public IList<Committente> Committenti
        {
            get { return _committenti; }
            set
            {
                _committenti = value;
                NotifyOfPropertyChange(() => Committenti);
            }
        }

        public EditFatturaViewModel EditFatturaViewModel
        {
            get { return Model.EditViewModel as EditFatturaViewModel; }
        }

        private string _onGridEmptyText = "Inserisci una nuova fattura";
        public string OnGridEmptyText
        {
            get { return _onGridEmptyText; }
            set { _onGridEmptyText = value; }
        }

        #endregion

        #region Initialization

        public override void CreateNewModel(DetachedCriteria queryByExample)
        {
            InitModel(queryByExample);
        }

        public override void CreateNewModel(int activeTab)
        {
            IsBusy = true;

            var pageProvider = new SearchFatturaQueryObject(this);
            var detachedCriteria = GetFattureByLastYearCriteria();
            pageProvider.DetachedCriteria = detachedCriteria.DetachedCriteria;
            const int pageSize = 100;
            CreateNewModel(pageSize, pageProvider, pageProvider, c => { CreateModel(activeTab, c);});
        }

        private void CreateModel(int activeTab, ObservableCollection<Core.Fattura> c)
        {

            SetUpNewModel(activeTab, c);

            //_currentDtoEntity.NotifyOfDataErrorInfo += OnDataErrorInfo;
            //var notifyPropertyChanged = _currentDtoEntity as PropertyChangedBase;
            //notifyPropertyChanged.PropertyChanged += OnPropChanged;

            //Model.Workspaces.CollectionChanged += OnWorkspacesChanged;
            //Model.SelectedPageChanged += OnPageChanged;
        }

        //private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    DecorateFatturaViewModels();
        //}

        //protected new void OnPageChanged(object o, PropertyChangeEventArgs data)
        //{
            //var tabViewModel = data.NewValue as ListViewViewModel;

            //if (tabViewModel == null) return;

            //if (tabViewModel is EditViewModel<T>)
            //    //default behavoir on page changed
            //    ((EditViewModel<T>)Model.EditViewModel).ShowCurrentEntity();
            //else
            //{
            //    if (!tabViewModel.NeedRefresh) return;
            //    Model.UserCollectionView.Refresh();
            //    tabViewModel.NeedRefresh = false;
            //}
        //}

        public override void CreateNewModel(QueryOver queryByExample)
        {
            InitModel(queryByExample.DetachedCriteria);
        }

        private void InitModel(DetachedCriteria queryByExample)
        {
            const int pageSize = 100;
            const int activeTab = 0;
            IsBusy = true;
            var pageProvider = new SearchFatturaQueryObject(this);
            pageProvider.DetachedCriteria = queryByExample;
            CreateNewModel(pageSize, pageProvider, pageProvider, c => { CreateModel(activeTab, c); });
        }

        private QueryOver GetFattureByLastYearCriteria()
        {
            var maxYear = Session.QueryOver<Core.Fattura>()
                .Select(
                    Projections.Max(Projections.SqlGroupProjection("YEAR(DataFatturaDB) As [Year]", "YEAR(DataFatturaDB)",
                        new[] {"YEAR"}, new IType[] {NHibernateUtil.Int32}))).Cacheable().SingleOrDefault<Int32>();

            QueryOver detachedCriteria;

            if (maxYear > 0)
            {
                var maxDate = new DateTime(maxYear, 12, 31);
                var minDate = new DateTime(maxYear, 01, 01);

                detachedCriteria = QueryOver.Of<Core.Fattura>().
                    Fetch(f => f.AnagraficaCedenteDB).Eager.Fetch(f => f.AnagraficaCommittenteDB).Eager.
                    Where(f => f.DataFatturaDB >= minDate).And(f => f.DataFatturaDB <= maxDate);
            }
            else
                detachedCriteria = QueryOver.Of<Core.Fattura>().
                    Fetch(f => f.AnagraficaCedenteDB).Eager.Fetch(f => f.AnagraficaCommittenteDB).Eager;
            return detachedCriteria;
        }

        #endregion
        
        #region  reference collection props

        #endregion
      
        #region entities events

        protected override void RegisterEntityAddedNewEvent()
        {
            //EventPublisher.Register<FatturaAddedNew>( RefreshAfterAddedNew );
        }

        //protected override void RegisterEntityUpdatedEvent()
        //{
        //    EventPublisher.Register<FatturaUpdated>( RefreshAfterUpdated );
        //}

        protected override QueryOver QueryCriteria
        {
            get { return QueryOver.Of<Core.Fattura>().Fetch(f=>f.AnagraficaCedenteDB).Eager.
                    Fetch(f=>f.AnagraficaCommittenteDB).Eager.
                OrderBy(f=>f.DataFatturaDB).Asc; }
            //.Cacheable().CacheMode(CacheMode.Normal)
        }

        //private void RefreshAfterAddedNew( FatturaAddedNew fattura )
        //{
        //    Model.UserEntities.Add( fattura.Dto );
        //    Model.UserCollectionView.MoveCurrentToLast();
        //}

        //private void RefreshAfterUpdated( FatturaUpdated fattura )
        //{
        //}

        #endregion

        #region command

        public void Initialize( Action<Presenter> onLoaded )
        {
            _onLoaded = onLoaded;
        }

        private Action<Presenter> _onLoaded;

        public void OnLoaded()
        {
            _onLoaded( this );

            //var _comuni = new ReferenceDataFactory( typeof( Comune ) ).ReferenceCollection ;

            var anagrafiche = new ReferenceDataFactory().GetReferenceCollection<Core.Anagrafica>(); 

            Fornitori = anagrafiche.Where(a => a is Fornitore).Cast<Fornitore>().
                OrderBy(f=>f.Denominazione + f.Cognome + f.Nome ).ToList();

            Committenti = anagrafiche.Where(a => a is Committente).Cast<Committente>().
                OrderBy(f => f.Denominazione + f.Cognome + f.Nome).ToList();
        }

        //private ICommand _openPdf;
        //public ICommand OpenPdf
        //{
        //    get
        //    {
        //        if ( _openPdf != null ) return _openPdf;
        //        _openPdf = new RelayCommand( param => OnOpendPdf(), param => true );
        //        return _openPdf;
        //    }

        //}

        private ICommand _search;
        public ICommand Search
        {
            get
            {
                if ( _search != null ) return _search;
                _search = new RelayCommand( param => OnSearch(), param => IsAllowFastSearchMod() );
                return _search;
            }

        }

        private void OnSearch()
        {
            SetActiveWorkSpace( 0 );
            var presenter = Presenters.CreateInstance( "SearchFattura", null ) as SearchFattura.Presenter;
            if ( presenter == null )
                throw new Exception();
            presenter.ConfirmResult += OnConfirmResultFlyFetch<Core.Fattura, Core.Fattura>;
            presenter.ShowDialog();
        }

        //private void OnOpendPdf()
        //{
        //    var model = Model.EditViewModel as EditFatturaViewModel;

        //    if ( model == null ) return;

        //    var currentEntity = model.CurrentEntity;
        //    if ( currentEntity == null ) return;

        //    Core.Fattura fattura = null;

        //    using ( var tx = Session.BeginTransaction() )
        //    {
        //        fattura = Session.QueryOver<Core.Fattura>().Where( f => f.Id == currentEntity.Id ).
        //            SingleOrDefault<Core.Fattura>();

        //        tx.Commit();
        //    }

        //    if ( fattura == null )
        //        return;

        //    var stream = fattura.PdfStream;
        //    if ( stream?.PdfStream == null || stream.PdfStream.Length == 0 ) return;
        //    var path = Path.GetTempFileName().Replace( "tmp", "PDF" );

        //    try
        //    {
        //        using ( var fs = File.Create( path ) )
        //        {
        //            fs.Write( stream.PdfStream, 0, stream.PdfStream.Length );
        //        }
        //        Process.Start( path );
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if ( fattura.PdfStream.Id > 0 )
        //            Session.Evict( Session.Get<DiskStream>( fattura.PdfStream.Id ) );
        //    }
        //}

        private ICommand _entityChoosenCommand;
        public ICommand EntityChoosenCommand
        {
            get
            {
                if ( _entityChoosenCommand != null )
                    return _entityChoosenCommand;
                _entityChoosenCommand = new RelayCommand( e => SwitchToDetailTabView(), o => true );
                return _entityChoosenCommand;
            }
        }

        private void SwitchToDetailTabView()
        {
            var tabViewModel = Model.Workspaces.FirstOrDefault( w => w is EditFatturaViewModel ) 
                as EditFatturaViewModel;

            if ( tabViewModel == null ) return;

            SetActiveWorkspace( tabViewModel );
        }

        private bool IsAllowDeleteMod()
        {
            var editFatturaViewModel = Model.EditViewModel as EditFatturaViewModel;
            return editFatturaViewModel != null && editFatturaViewModel.AllowDelete;
        }

        private bool IsAllowFastSearchMod()
        {
            var editFatturaViewModel = Model.EditViewModel as EditFatturaViewModel;
            return editFatturaViewModel != null && editFatturaViewModel.AllowFastSearch;
        }

        #endregion

        #region INotifyDataSourceHit Members

        protected override BaseCrudModel CreateNewModel()
        {
            var model = new Model();
            return model;
        }

        public override void QueryInProgress(bool inProgress)
        {
            //Model.EditViewModel. = inProgress;
        }

        #endregion

        //private IList<Core.Anagrafica> LoadReferences()
        //{
        //    IList<Core.Anagrafica> anagrafiche = new List<Core.Anagrafica>();
        //    using (var tx = Session.BeginTransaction())
        //    {
        //        var fornitori = QueryOver.Of<Core.Anagrafica>();

        //        var results = Session.CreateMultiCriteria().
        //            Add(fornitori).
        //            //Add(causali).Add(pods).
        //            List();

        //        var result = (results[0] as IList) as List<Core.Anagrafica>;
        //        if ( result != null)
        //        {
        //            anagrafiche = result;                   
        //        }

        //        tx.Commit();
        //    }
        //    return anagrafiche;
        //}

        private void OnCancelEditOnViewModel( RemoveTabView vm )
        {
            if ( !Model.EditViewModel.Equals(vm.ParentViewModel))
                return;

            var vmType = vm.ViewModel.GetType();

            vm.ParentViewModel.Load();

            SetActiveWorkSpace( 1 );

            var tabViewModel = Workspaces.FirstOrDefault( w => w.GetType() == vmType );
            if ( tabViewModel != null )
                Workspaces.Remove( tabViewModel );
        }

    }
}