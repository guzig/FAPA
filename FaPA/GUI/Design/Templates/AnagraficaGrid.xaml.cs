using System.Windows.Input;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per AnagraficaGrid.xaml
    /// </summary>
    public partial class AnagraficaGrid
    {
        public AnagraficaGrid()
        {
            InitializeComponent();
            GridControl = AnagraficheGridControl;
            RecordsToolBar = recordsToolBar;
            EmptyMessage = emptyMessage;
        }

        private void AnagraficheGridControl_OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            //DataGridHelpers.DataGridKeyUpEventHandler( e, CategoriaGridControl );
        }
    }
}
