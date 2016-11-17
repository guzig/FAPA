using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Helpers;

namespace FaPA.GUI.Feautures.SearchAnagrafica
{
    public class Presenter: AbstractPresenter<Model, View>
    {
        private readonly BackgroundWorker _queryBackgroundWorker;

        private IList<Core.Anagrafica> _liquidazioniFounds;
        private bool _isSelectedAll;

        public Presenter()
        {
            View.Presenter = this;
            _queryBackgroundWorker = new BackgroundWorker();
            _queryBackgroundWorker.DoWork += (sender, args) => PerformActualQuery();
            _queryBackgroundWorker.RunWorkerCompleted += (sender, args) => CompleteQuery();
            Model = new Model();
            View.AnagraficaGridSearch.SelectionChanged += (s, e) =>
            { Model.SelectedItemsCount.Value = View.AnagraficaGridSearch.SelectedItems.Count; };
        }

        #region View binding stuff

        public Fact CanConfirmAllResult
        {
            get { return new Fact( Model.ResultEntryCount, () => Model.ResultEntryCount.Value > 0 ); }

        }

        public void OnConfirmAllResult()
        {
            Model.IsBusy.Value = true;

            if ( View.AnagraficaGridSearch.SelectedItems.Count <= 0 ) return;

            if ( !Model.AnagraficaFinder.CreateDetachedQuery() ) return;

            OnConfirmResult( new FinderConfirmSearchEventArgs( Model.Anagrafiche,
                Model.AnagraficaFinder.DetachedQueryCriteria, Model.AllowedGridProperties ) );

            Model.IsBusy.Value = false;

            View.Close();
        }

        public Fact CanCancel
        {
            get
                {
                    return new Fact( Model.AllowEditing, () => Model.AllowEditing.As<bool>( arg => arg ) );
        
                }
        }

        public void OnCancel()
        {
            View.Close();
        }

        public Fact CanQuery
        {
            get
            {
                return new Fact( Model.AllowSearch, () => Model.AnagraficaFinder.IsValid &&
                                                         Model.AllowEditing.As<bool>( arg => arg ) &&
                                                         Model.AllowSearch.As<bool>( arg => arg ) );
            }
        }

        public void OnQuery()
        {

            ShowCursor.Show();

            Model.AllowSearch.Value = false;
            Model.AllowEditing.Value = false;
            Model.IsBusy.Value = true;
            Model.AllowConfirmResult.Value = false;
            Model.SelectedItemsCount.Value = 0;
            Model.ResultEntryCount.Value = 0;

            _queryBackgroundWorker.RunWorkerAsync();
        }

        public Fact CanConfirmSearchResults
        {
            get
            {
                return new Fact( Model.AllowConfirmResult, () => Model.AllowConfirmResult.As<bool>( arg => arg ) &&
                                                                Model.SelectedItemsCount.As<int>( arg => arg > 0 ) );
            }
        }

        public void OnConfirmSearchResults()
        {
            Model.IsBusy.Value = true;
            if ( View.AnagraficaGridSearch.SelectedItems.Count <= 0 ) return;

            if ( _isSelectedAll )
            {
                if ( !Model.AnagraficaFinder.CreateDetachedQuery() ) return;
                OnConfirmResult( new FinderConfirmSearchEventArgs( Model.Anagrafiche,
                    Model.AnagraficaFinder.DetachedQueryCriteria, Model.AllowedGridProperties ) );
            }
            else
            {
                IList result = View.AnagraficaGridSearch.SelectedItems.OfType<Core.Anagrafica>().ToList();
                OnConfirmResult( new FinderConfirmSearchEventArgs( result, null, true, Model.AllowedGridProperties ) );
            }
            Model.IsBusy.Value = false;
            View.Close();
        }

        public Fact CanSelectAll
        {
            get
            {
                return new Fact( Model.SelectedItemsCount, () => Model.ResultEntryCount.Value > 0 &&
                    Model.SelectedItemsCount.As<int>(arg => arg < Model.ResultEntryCount ) );
            }
        }

        public void OnSelectAll()
        {
            _isSelectedAll = true;
            View.AnagraficaGridSearch.UnselectAll();
            View.AnagraficaGridSearch.SelectAll();
        }

        public Fact CanDeselectAll
        {
            get { return new Fact( Model.SelectedItemsCount, () => Model.SelectedItemsCount.As<int>( arg => arg > 0 ) ); }
        }

        public void OnDeselectAll()
        {
            View.AnagraficaGridSearch.UnselectAll();
        }

        public Fact CanClearSearch()
        {
            return new Fact( Model.AllowEditing, () => Model.AllowEditing.As<bool>( arg => arg ) );
        }

        public void OnClearSearch()
        {
            Model.AnagraficaFinder.ClearSearchParamValues();
            Model.AnagraficheView=CollectionViewSource.GetDefaultView( new ObservableCollection<Core.Anagrafica>(
                new List<Core.Anagrafica>() ) ) ;
        }

        #endregion

        private void CompleteQuery()
        {
            if ( _liquidazioniFounds == null )
                _liquidazioniFounds = new Core.Anagrafica[] { };

            Model.AnagraficheView = CollectionViewSource.GetDefaultView( new ObservableCollection<Core.Anagrafica>(
                _liquidazioniFounds ) );

            Model.AllowSearch.Value = true;
            Model.IsBusy.Value = false;
            Model.AllowEditing.Value = true;
            Model.AllowConfirmResult.Value = _liquidazioniFounds.Count > 0;
        }

        private void PerformActualQuery()
        {
            if (!Model.AnagraficaFinder.CreateDetachedQuery()) return;

            _liquidazioniFounds = GetExeCriteriaAsReadOnly<Core.Anagrafica>( Model.AnagraficaFinder.DetachedQueryCriteria );

            Session.Clear();

            Model.ResultEntryCount.Value = _liquidazioniFounds.Count;
        }

        public override void Dispose()
        {
            _queryBackgroundWorker.Dispose();
            base.Dispose();
        }

        public delegate void ConfirmSearchHandler(object sender, FinderConfirmSearchEventArgs data);

        public event ConfirmSearchHandler ConfirmResult;

        private void OnConfirmResult(FinderConfirmSearchEventArgs data)
        {
            var handler = ConfirmResult;
            if (handler != null)
            {
                handler(this, data);
                
            }
        }
    }
}
