using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using FaPA.Core;
using FaPA.Infrastructure.Helpers;

namespace FaPA.GUI.Utils
{
    public static class DataGridHelpers
    {

        public static void DataGridKeyUpEventHandler( KeyEventArgs e, DataGrid grid )
        {
            switch ( e.Key )
            {
                //case Key.Tab:
                //case Key.Enter:
                //    grid.CommitEdit();
                //    return;
                case Key.Down:
                    grid.CommitEdit();
                    if ( grid.SelectedIndex == grid.Items.Count - 1 )
                    {
                        grid.SelectedIndex--;
                        grid.SelectedIndex = 0;
                        DataGridScrollIntoView( grid, 0 );
                        return;
                    }
                    if ( grid.SelectedIndex > 0 )
                    {
                        grid.SelectedIndex--;
                    }
                    grid.SelectedIndex++;
                    break;
                case Key.Up:
                    grid.CommitEdit();
                    if ( grid.SelectedIndex == 0 )
                    {
                        var selectedIndex = grid.Items.Count - 1;
                        grid.SelectedIndex = selectedIndex;
                        DataGridScrollIntoView( grid, selectedIndex );
                        return;
                    }
                    if ( grid.SelectedIndex < grid.Items.Count )
                    {
                        grid.SelectedIndex++;
                    }
                    grid.SelectedIndex--;
                    break;
            }
        }

        public static void ListViewKeyUpEventHandler( KeyEventArgs e, ListView grid )
        {
            switch ( e.Key )
            {
                //case Key.Tab:
                //case Key.Enter:
                //    grid.CommitEdit();
                //    return;
                case Key.Down:
                    if ( grid.SelectedIndex == grid.Items.Count - 1 )
                    {
                        grid.SelectedIndex = 0;
                        return;
                    }
                    grid.SelectedIndex--;
                    grid.SelectedIndex++;
                    break;
                case Key.Up:

                    if ( grid.SelectedIndex == 0 )
                    {
                        grid.SelectedIndex = grid.Items.Count - 1;
                        return;
                    }
                    grid.SelectedIndex++;
                    grid.SelectedIndex--;
                    break;
            }
        }

        public static void ApplyGroup( string propName, ICollectionView view )
        {
            var exist = view.GroupDescriptions.Cast<PropertyGroupDescription>().
                Any( f => f.PropertyName == propName );

            if ( exist )
            {
                SystemSounds.Beep.Play();
                return;
            }
            ShowCursor.Show();
            ProxyHelpers.UnproxiedCollection<BaseEntity>( view.SourceCollection.Cast<BaseEntity>() );
            var propertyGroupDescription = new PropertyGroupDescription( propName );
            view.GroupDescriptions.Add( propertyGroupDescription );
        }

        public static void ApplyFilter( string filterValue, ICollectionView gridItemSource, string filterProp )
        {
            if ( string.IsNullOrWhiteSpace( filterValue ) )
                return;

            ShowCursor.Show();

            var collectionView = gridItemSource;

            ProxyHelpers.UnproxiedCollection<BaseEntity>( gridItemSource.SourceCollection.Cast<BaseEntity>() );

            collectionView.Filter = item =>
            {
                if ( item == null ) return false;
                var propertyInfo = GetPropertyType( item, filterProp );
                var propValue = GetPropertyValue( item, filterProp );
                if ( propertyInfo.PropertyType == typeof( DateTime ) || propertyInfo.PropertyType == typeof( DateTime? ) )
                {
                    return ( ( DateTime ) propValue ).ToShortDateString() == filterValue.Replace( ".", "/" );
                }
                return propValue != null && propValue.ToString() == filterValue;
            };

            if ( collectionView.IsEmpty )
            {
                gridItemSource.Filter = item => true;
            }
        }

        private static object GetPropertyValue( object obj, string propertyName )
        {
            var _propertyNames = propertyName.Split( '.' );

            for ( var i = 0; i < _propertyNames.Length; i++ )
            {
                if ( obj != null )
                {
                    var _propertyInfo = obj.GetType().GetProperty( _propertyNames[i] );
                    if ( _propertyInfo != null )
                        obj = _propertyInfo.GetValue( obj );
                    else
                        obj = null;
                }
            }

            return obj;
        }


        private static PropertyInfo GetPropertyType( object obj, string propertyName )
        {
            var _propertyNames = propertyName.Split( '.' );

            PropertyInfo propertyInfo = null;
            for ( var i = 0; i < _propertyNames.Length; i++ )
            {
                if ( obj != null )
                {
                    propertyInfo = obj.GetType().GetProperty( _propertyNames[i] );
                    if ( propertyInfo != null )
                        obj = propertyInfo.GetValue( obj );
                    else
                        obj = null;
                }
            }

            return propertyInfo;
        }



        public static void DisableButtons( Button btnGroupBy, Button btnFilterBy, Button btnClearFilters, Button btnClearGroup )
        {
            ShowCursor.Show();

            btnGroupBy.IsEnabled = false;
            btnFilterBy.IsEnabled = false;
            btnClearFilters.IsEnabled = false;
            btnClearGroup.IsEnabled = false;
        }

        public static void EnableButtons( Button btnApplyGroup, Button btnApplyFiltr, Button btnClerFilter, Button btnClearGroups )
        {
            btnApplyGroup.IsEnabled = true;
            btnApplyFiltr.IsEnabled = true;
            btnClerFilter.IsEnabled = true;
            btnClearGroups.IsEnabled = true;
        }

        public static void DataGridScrollIntoView( DataGrid dataGrid, int indexRow )
        {
            dataGrid.Focus();
            if ( dataGrid.Items.Count == 0 )
                return;
            dataGrid.CommitEdit();
            dataGrid.SelectedItem = indexRow;
            dataGrid.ScrollIntoView( indexRow );
            var dgrow = GetDataGridRow( dataGrid, indexRow );
            dgrow?.MoveFocus( new TraversalRequest( FocusNavigationDirection.Next ) );
        }

        private static DataGridRow GetDataGridRow( DataGrid dataGrid, int indexRow )
        {
            var item = dataGrid.Items[indexRow];
            if ( item == null ) return null;
            var dgrow = ( DataGridRow ) dataGrid.ItemContainerGenerator.ContainerFromItem( item );
            return dgrow;
        }

        public static void DataGridScrollIntoBottomView( DataGrid dataGrid )
        {
            dataGrid.Focus();
            if ( dataGrid.Items.Count == 0 )
                return;
            dataGrid.CommitEdit();
            int index = dataGrid.Items.Count - 1;
            dataGrid.SelectedItem = dataGrid.Items[index];
            dataGrid.ScrollIntoView( dataGrid.Items[index] );
            var dgrow = GetDataGridRow( dataGrid, index );
            dgrow?.MoveFocus( new TraversalRequest( FocusNavigationDirection.Next ) );
            dataGrid.BeginEdit();
        }

        public static void DataGridEditEnding( object sender, DataGridRowEditEndingEventArgs e, FrameworkElement crossCoupledProps )
        {
            var dataGrid = sender as DataGrid;
            if ( e.EditAction != DataGridEditAction.Commit ) return;

            var view = CollectionViewSource.GetDefaultView( dataGrid.ItemsSource ) as ListCollectionView;
            if ( view.IsAddingNew || view.IsEditingItem )
            {
                dataGrid.Dispatcher.BeginInvoke( new DispatcherOperationCallback( param =>
                {
                    // This callback will be called after the UserEntitiesView
                    // has pushed the changes back to the DataGrid.ItemSource.

                    // Write code here to save the data to the database.

                    //MoveFocusRowGrid(dataGrid);

                    crossCoupledProps.BindingGroup?.CommitEdit();

                    return null;
                } ), DispatcherPriority.Background, new object[] { null } );
            }
        }


    }
}
