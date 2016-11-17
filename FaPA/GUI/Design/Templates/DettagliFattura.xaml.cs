using System.Windows.Input;

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
            //DataGridHelpers.DataGridKeyUpEventHandler( e, DettagliFatturaGridControl );
        }

        //private void OnExpanded(object sender, RoutedEventArgs e)
        //{
        //   ShowCursor.Show();
        //}



    }
}
