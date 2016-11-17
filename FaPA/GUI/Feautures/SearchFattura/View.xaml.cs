using System.Windows;
using System.Windows.Input;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Feautures.SearchFattura
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : WindowBase
    {
        public Presenter Presenter { get; set; }

        public View()
        {

            InitializeComponent();
            //FatturaFinder.ExpLiquidazione.Expanded += ExpanderExpanded;
            //FatturaFinder.ExpPoD.Expanded += ExpanderExpanded;
            //FatturaFinder.ExpOthersMain.Expanded += ExpanderExpanded;
            //FatturaFinder.ExpOthers.Expanded += ExpanderExpanded;
        }
        
        private void ExpanderExpanded(object sender, RoutedEventArgs e)
        {
            var scrollViwer = ScrollViewer;

            if (scrollViwer != null)
            {
                // Logical Scrolling by Item
                // scrollViwer.LineUp();
                // Physical Scrolling by Offset
                scrollViwer.ScrollToVerticalOffset(scrollViwer.VerticalOffset + 210);
            }

        }

        private void FattureGridSearch_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            //DataGridHelpers.DataGridKeyUpEventHandler( e, FattureGridSearch );
        }
    }
}
