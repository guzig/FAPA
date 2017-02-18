using System.Windows.Input;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for DettagliFattura.xaml
    /// </summary>
    public partial class DettagliFattura
    {

        public DettagliFattura()
        {
            InitializeComponent();
            //GridControl = DettagliFatturaGridControl;
            //RecordsToolBar = recordsToolBar;
            //EmptyMessage = emptyMessage;
        }

        private void DettagliFatturaGridControl_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, DettagliFatturaGridControl );
        }


        //private void OnExpanded(object sender, RoutedEventArgs e)
        //{
        //   ShowCursor.Show();
        //}

        //private void DettagliFatturaGridControl_OnAddingNewItem( object sender, AddingNewItemEventArgs e )
        //{dataGrid1.Focus()
        //    dataGrid1.CurrentCell = new DataGridCellInfo(
        //        dataGrid1.Items[0], dataGrid1.Columns[3] );
        //    dataGrid1.BeginEdit();
        //}
    }
}
