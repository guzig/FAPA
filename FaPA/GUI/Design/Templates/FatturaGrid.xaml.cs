using System.Windows.Input;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per FatturaGrid.xaml
    /// </summary>
    public partial class FatturaGrid
    {
        public FatturaGrid()
        {
            InitializeComponent();
            GridControl = FattureGridControl;
            RecordsToolBar = recordsToolBar;
            EmptyMessage = emptyMessage;
        }

        private void FattureGridControl_OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            //DataGridHelpers.DataGridKeyUpEventHandler( e, CategoriaGridControl );
        }
    }
}
