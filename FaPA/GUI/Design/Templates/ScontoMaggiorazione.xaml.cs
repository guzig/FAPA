using System.Windows.Controls;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per AltriDatiGrid.xaml
    /// </summary>
    public partial class ScontoMaggiorazione
         
    {
        public ScontoMaggiorazione()
        {
            InitializeComponent();
            //GridControl = ScontoMaggiorazioneGridControl;
            //RecordsToolBar = recordsToolBar;
            //EmptyMessage = emptyMessage;
        }


        private void DgRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            WpfHelpers.DataGridEditEnding(sender, e, ScontoMaggiorazioneGridControl);
        }

    }
}
