using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per AnagraficaGrid.xaml
    /// </summary>
    public partial class AnagraficaGrid
    {
        private readonly IDictionary<string, string> _columns = new Dictionary<string, string>();

        public AnagraficaGrid()
        {
            InitializeComponent();
            SetUpGrid();
            AnagraficheGridControl.PreviewKeyDown += AnagraficheGridControl_OnPreviewKeyDown;
            AnagraficheGridControl.Sorting += OnSorting;

        }

        private void AnagraficheGridControl_OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, AnagraficheGridControl );
        }

        private static void OnSorting( object sender, DataGridSortingEventArgs e )
        {
            ShowCursor.Show();
            var dgrid = sender as DataGrid;
            NhProxyHelpers.UnproxiedItems( dgrid );
        }

        private void SetUpGrid()
        {
            foreach ( var column in AnagraficheGridControl.Columns )
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
            DataGridHelpers.ApplyFilter( txtFilter.Text, AnagraficheGridControl.Items, _columns, cmbProperty.Text );
            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }

        private void btnClear_Click( object sender, RoutedEventArgs e )
        {
            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
            AnagraficheGridControl.Items.Filter = item => true;
            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }

        private void btnGroup_Click( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrWhiteSpace( cmbGroups.Text ) ) return;

            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );

            DataGridHelpers.ApplyGroup( _columns, cmbGroups.Text, AnagraficheGridControl.Items );

            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }

        private void btnClearGr_Click( object sender, RoutedEventArgs e )
        {
            DataGridHelpers.DisableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
            AnagraficheGridControl.Items.GroupDescriptions.Clear();
            DataGridHelpers.EnableButtons( btnGroup, btnApplyFilter, btnClearFilter, btnClearGroup );
        }
    }
}
