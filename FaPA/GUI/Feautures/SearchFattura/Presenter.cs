using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Caliburn.Micro;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using FaPA.Infrastructure.FlyFetch;
using FaPA.Infrastructure.Helpers;
using Action = System.Action;

namespace FaPA.GUI.Feautures.SearchFattura
{
    public class Presenter : AbstractPresenter<Model, View>, INotifyDataSourceHit, INotifyDataSourceLoadCompleted
    {
        private bool _isSelectedAll;
        private bool _mouseClicked;
        public SearchFatturaQueryObject SearchFatturaPageProvider { get; private set; }
        public delegate void ConfirmSearchHandler(object sender, FinderConfirmSearchEventArgs data);
        public event ConfirmSearchHandler ConfirmResult;
        private Action _onLoadedCollection; 

        //ctor
        public Presenter()
        {
            View.Presenter = this;
            View.FattureGridSearch.SelectionChanged += OnSelectionChanged;
            View.FattureGridSearch.PreviewMouseDown += OnMouseDown;
            
            //View.FattureGridSearch.CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, OnSelectAllClick));
            
            View.FattureGridSearch.Sorting += UnproxyCollection;
                             
            Model = new Model();
            SearchFatturaPageProvider = new SearchFatturaQueryObject(this, this);

            View.FattureGridSearch.SelectionChanged += (s, e) => { Model.SelectedEntryCount.Value = 
                View.FattureGridSearch.SelectedItems.Count; };
        }

        private void UnproxyCollection(object sender, DataGridSortingEventArgs e)
        {
            UnproxyCollection();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _mouseClicked = true;
        }

        private void OnReportError(Exception exc)
        {
            Execute.OnUIThread(() => MessageBox.Show(GetMessage(exc), "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error));
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
                return new Fact(Model.AllowSearch, () => Model.FattureFinder.IsValid && 
                                                         Model.AllowEditing.As<bool>(arg => arg) &&
                                                         Model.AllowSearch.As<bool>(arg => arg));
            }
        }

        public void OnQuery()
        {
            Model.AllowSearch.Value = false;
            Model.AllowEditing.Value = false;
            Model.AllowConfirmResult.Value = false;
            Model.SelectedEntryCount.Value = 0;
            Model.IsBusy.Value = true;

            PerformActualQuery();
        }

        public Fact CanConfirmSearchResults
        {
            get
            {
                return new Fact(Model.AllowConfirmResult, () => Model.AllowConfirmResult.As<bool>(arg => arg) &&
                                                                Model.SelectedEntryCount.As<int>(arg => arg > 0));
            }
        }
       
        public void OnConfirmSearchResults()
        {
            Model.IsBusy.Value = true;
            if (View.FattureGridSearch.SelectedItems.Count <= 0) return;
            
            if ( _isSelectedAll )
            {
                if (!Model.FattureFinder.CreateDetachedQuery()) return;
                OnConfirmResult(new FinderConfirmSearchEventArgs(Model.PagedCollection, 
                    Model.FattureFinder.DetachedQueryCriteria, Model.AllowedGridProperties));
            }
            else
            {
                IList result = View.FattureGridSearch.SelectedItems.OfType<Core.Fattura>().ToList();
                OnConfirmResult(new FinderConfirmSearchEventArgs(result,null, true, Model.AllowedGridProperties));
            }
            Model.IsBusy.Value = false;
            View.Close();
        }

        public Fact CanSelectAll
        {
            get
            {
                return new Fact(Model.SelectedEntryCount, () => Model.ResultEntryCount.Value > 0 && 
                    Model.SelectedEntryCount.As<int>(arg => arg < Model.ResultEntryCount));
            }
        }
        
        public void OnSelectAll()
        {
            _isSelectedAll = true;
            View.FattureGridSearch.UnselectAll();
            OnSelectedItems();
        }
        
        public Fact CanDeselectAll
        {
            get
            {
                return new Fact(Model.SelectedEntryCount, () => Model.SelectedEntryCount.As<int>(arg => arg > 0));
            }
        }
        
        public void OnDeselectAll()
        {
            View.FattureGridSearch.UnselectAll();
        }

        public Fact CanClearSearch
        {
            get
            {
                return new Fact( Model.AllowEditing, () => Model.AllowEditing.As<bool>( arg => arg ) );
                
            }
        }

        public void OnClearSearch()
        {
            Model.FattureFinder.ClearSearchParamValues();
            Model.Fatture = CollectionViewSource.GetDefaultView( 
                new ObservableCollection<Core.Fattura>( new List<Core.Fattura>() ) );
        }

        public Fact CanConfirmAllResult
        {
            get { return new Fact( Model.ResultEntryCount, () => Model.ResultEntryCount.Value > 0 ); }
            
        }

        public void OnConfirmAllResult()
        {
            Model.IsBusy.Value = true;

            if ( View.FattureGridSearch.SelectedItems.Count <= 0 ) return;

            if ( !Model.FattureFinder.CreateDetachedQuery() ) return;

            OnConfirmResult( new FinderConfirmSearchEventArgs( Model.PagedCollection,
                Model.FattureFinder.DetachedQueryCriteria, Model.AllowedGridProperties ) );

            Model.IsBusy.Value = false;

            View.Close();
        }

        private void OnConfirmResult( FinderConfirmSearchEventArgs data )
        {
            var handler = ConfirmResult;

            if ( handler != null ) handler( this, data );
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (_mouseClicked)
                if (selectionChangedEventArgs.AddedItems.Count > 0)
                {
                    _isSelectedAll = selectionChangedEventArgs.AddedItems.Count + 1 == Model.PagedCollection.Count;
                    OnSelectedItems();
                }                  
            _mouseClicked = false;
        }
        
        private void OnSelectAllClick(object sender, ExecutedRoutedEventArgs e)
        {
            OnSelectAll();
        }
        
        private void OnSelectedItems()
        {
            _onLoadedCollection = () =>
            {
                Model.AllowSearch.Value = true;
                Model.AllowEditing.Value = true;
                _onLoadedCollection = null;
                if ( _isSelectedAll )
                    View.FattureGridSearch.SelectAll();
            };

            Model.AllowSearch.Value = false;
            Model.AllowEditing.Value = false;

            UnProxySelectedItems(_isSelectedAll,Model.PagedCollection, View.FattureGridSearch, _onLoadedCollection);
        }
        
        private void PerformActualQuery()
        {
            ShowCursor.Show();
            if (!Model.FattureFinder.CreateDetachedQuery())
                return;
            Model.Fatture = null;
            SearchFatturaPageProvider.DetachedCriteria = Model.FattureFinder.DetachedQueryCriteria;
            CollectionFactory.Create(100
                                      , SearchFatturaPageProvider
                                      , SearchFatturaPageProvider
                                      , created =>
                                          {
                                              Model.PagedCollection = created;
                                              Model.ResultEntryCount.Value = Model.PagedCollection.Count;
                                              Model.Count = Model.PagedCollection.Count;
                                              Model.AllowConfirmResult.Value = Model.Count > 0;
                                              Model.Fatture = CollectionViewSource.GetDefaultView(created);
                                              //Model.Fatture = created;
                                              Model.AllowEditing.Value = true;
                                              Model.AllowSearch.Value = true;
                                              Model.IsBusy.Value = false;                                             
                                          });
        }

        public void UnproxyCollection()
        {
            if ( SearchFatturaPageProvider.IsFetchCompleted ) return;
            ProxyHelpers.UnproxiedCollection( Model.PagedCollection );
        }

        private static string GetMessage(Exception exc)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Error occured:");
            do
            {
                sb.AppendLine(exc.Message);
                exc = exc.InnerException;
            } while (exc != null);
            return sb.ToString();
        }
        
        #region INotifyDataSourceHit Members

        public void QueryInProgress(bool inProgress)
        {
            Model.DataSourceHit = inProgress;
        }

        #endregion
  
        #region INotifyDataSourceLoadCompleted Members

        public void LoadCompleted(bool loadCompleted)
        {
            Model.IsFetchCompleted = loadCompleted;

            if (Model.IsFetchCompleted && _onLoadedCollection != null)
                _onLoadedCollection();

        }

        #endregion
    }
}
