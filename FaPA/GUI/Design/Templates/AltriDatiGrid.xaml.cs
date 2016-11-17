using System.Windows.Controls;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per AltriDatiGrid.xaml
    /// </summary>
    public partial class AltriDatiGrid 
    {
        public AltriDatiGrid()
        {
            InitializeComponent();
            //GridControl = AltriDatiGridControl;
            //RecordsToolBar = recordsToolBar;
            //EmptyMessage = emptyMessage;
        }


        private void DgRowEditEnding( object sender, DataGridRowEditEndingEventArgs e )
        {
            WpfHelpers.DataGridEditEnding( sender, e, AltriDatiGridControl );
        }

    }
}
