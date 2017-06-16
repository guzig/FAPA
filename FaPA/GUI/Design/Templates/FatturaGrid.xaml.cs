using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per FatturaGrid.xaml
    /// </summary>
    public partial class FatturaGrid
    {
        private readonly IDictionary<string, string> _columns = new Dictionary<string, string>();

        public FatturaGrid()
        {
            InitializeComponent();
            SetUpGrid();
            FattureGridControl.PreviewKeyDown += FattureGridControl_OnPreviewKeyDown;
            FattureGridControl.Sorting += OnSorting;

        }

        private void FattureGridControl_OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, FattureGridControl );
        }

        private static void OnSorting( object sender, DataGridSortingEventArgs e )
        {
            ShowCursor.Show();
            var dgrid = sender as DataGrid;
            NhProxyHelpers.UnproxiedItems( dgrid );
        }

        private void SetUpGrid()
        {
            foreach ( var column in FattureGridControl.Columns )
            {
                _columns.Add( column.Header.ToString(), column.SortMemberPath );
            }

            cmbProperty.ItemsSource = _columns.Keys;
            cmbGroups.ItemsSource = _columns.Keys;
            //cmbProperty.SelectionChanged += (s, e) => OnMyComboBoxChanged(s,e, btnGroup);
        }


        private void btnFilter_Click( object sender, RoutedEventArgs e )
        {
            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
            DataGridHelpers.ApplyFilter( txtFilter.Text, FattureGridControl.Items, _columns[cmbProperty.Text] );
            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }

        private void btnClear_Click( object sender, RoutedEventArgs e )
        {
            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
            FattureGridControl.Items.Filter = item => true;
            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }

        private void btnGroup_Click( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrWhiteSpace( cmbGroups.Text ) ) return;

            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );

            DataGridHelpers.ApplyGroup( _columns[cmbGroups.Text], FattureGridControl.Items );

            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }

        private void btnClearGr_Click( object sender, RoutedEventArgs e )
        {
            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
            FattureGridControl.Items.GroupDescriptions.Clear();
            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }


    }

}
