using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace FaPA.GUI.Controls
{
    public class BaseListViewControl : UserControl, IDisposable

    {
        public static readonly DependencyProperty GridItemSourceProperty =
            DependencyProperty.Register( "GridItemSource", typeof( ICollectionView ), typeof( BaseListViewControl ),
            new FrameworkPropertyMetadata( null, OnItemsSourceChanged ) );

        public static DependencyProperty IsSynchronizedWithCurrentItemProperty =
            DependencyProperty.Register( "IsSynchronizedWithCurrentItem", typeof( bool ), typeof( BaseListViewControl ) );

        private static void OnItemsSourceChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var baseListViewControl = d as BaseListViewControl;

            var collectionView = e.NewValue as ICollectionView;

            if ( baseListViewControl?.GridControl != null && collectionView != null )
                baseListViewControl.GridControl.ItemsSource = collectionView;

            if (baseListViewControl != null)
                SetGridVisibility( baseListViewControl.GridControl );
        }

        public ICollectionView GridItemSource
        {
            get
            {
                return ( ICollectionView ) GetValue( GridItemSourceProperty );
            }
            set
            {
                SetValue( GridItemSourceProperty, value );
                OnItemsSourceChanged( this, new DependencyPropertyChangedEventArgs( GridItemSourceProperty, value, value ) );
            }

        }

        public bool IsSynchronizedWithCurrentItem
        {
            get { return ( bool ) GetValue( IsSynchronizedWithCurrentItemProperty ); }
            set { SetValue( IsSynchronizedWithCurrentItemProperty, value ); }
        }

        private Selector _gridControl;
        protected Selector GridControl
        {
            get { return _gridControl; }
            set
            {
                _gridControl = value;
                _gridControl.Loaded += OnGridLoaded;
                var collectionView = _gridControl.Items.SourceCollection as CollectionView;
                if ( collectionView != null )
                    collectionView.CurrentChanged += SetDataGridView;
            }
        }

        private void OnGridLoaded( object sender, RoutedEventArgs routedEventArgs )
        {
            var dataGrid = sender as DataGrid;
            if (EmptyMessage != null)
                EmptyMessage.Visibility = dataGrid != null && dataGrid.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;

        }

        private void SetDataGridView( object sender, EventArgs eventArgs )
        {
            var collection = sender as ItemCollection;

            SetContextVisibility( collection );
        }

        private void SetContextVisibility( ItemCollection collection )
        {
            if ( collection == null || EmptyMessage == null) return;

            if ( collection.Count > 0 )
            {
                if ( GridControl.Visibility == Visibility.Visible )
                    return;
                EmptyMessage.Visibility = Visibility.Collapsed;
                GridControl.Visibility = Visibility.Visible;
                RecordsToolBar.Visibility = Visibility.Visible;
            }
            else
            {
                if ( GridControl.Visibility == Visibility.Collapsed )
                    return;
                EmptyMessage.Visibility = Visibility.Visible;
                GridControl.Visibility = Visibility.Collapsed;
                RecordsToolBar.Visibility = Visibility.Collapsed;
            }
        }

        private static void SetGridVisibility( Selector gridControl )
        {
            if ( gridControl == null )
                return;

            if ( gridControl.Items.Count > 0 )
            {
                if ( gridControl.Visibility == Visibility.Visible )
                    return;

                gridControl.Visibility = Visibility.Visible;
            }
            else
            {
                if ( gridControl.Visibility == Visibility.Collapsed )
                    return;

                gridControl.Visibility = Visibility.Collapsed;
            }
        }

        private void SetEmptyMessageVisibility( DockPanel emptyMessage )
        {
            if ( GridItemSource == null )
                return;

            emptyMessage.Visibility = GridItemSource == null || GridItemSource.IsEmpty ? Visibility.Collapsed : Visibility.Visible;
        }

        private DockPanel _recordsToolBar;
        private DockPanel _emptyMessage;

        protected DockPanel RecordsToolBar
        {
            get { return _recordsToolBar; }
            set
            {
                _recordsToolBar = value;
                _recordsToolBar.DataContext = this;
            }
        }

        protected DockPanel EmptyMessage
        {
            get { return _emptyMessage; }
            set
            {
                _emptyMessage = value;
                SetEmptyMessageVisibility( value );
            }
        }

        /// <summary>
        /// Esegue attività definite dall'applicazione, come rilasciare o reimpostare risorse non gestite.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            var collectionView = _gridControl.Items.SourceCollection as CollectionView;
            if ( collectionView != null )
                collectionView.CurrentChanged -= SetDataGridView;

        }
    }
}