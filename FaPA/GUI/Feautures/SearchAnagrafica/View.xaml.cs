using System.Windows;
using System.Windows.Input;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Feautures.SearchAnagrafica
{
    /// <summary>
    /// Logica di interazione per View.xaml
    /// </summary>
    public partial class View : WindowBase
    {
        public Presenter Presenter { get; set; }

        public View()
        {
            InitializeComponent();

            //AnagraficaSearchTemplate.ImpegnoExpander.Expanded += ExpanderExpanded;

        }

        private void ExpanderExpanded( object sender, RoutedEventArgs e )
        {
            var scrollViwer = AnagraficaScrollViewer;

            if ( scrollViwer != null )
            {
                // Logical Scrolling by Item
                // scrollViwer.LineUp();
                // Physical Scrolling by Offset
                scrollViwer.ScrollToVerticalOffset( scrollViwer.VerticalOffset + 210 );
            }

        }

        private void AnagraficaGridSearch_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            //DataGridHelpers.DataGridKeyUpEventHandler( e, AnagraficaGridSearch );
        }
    }
}
