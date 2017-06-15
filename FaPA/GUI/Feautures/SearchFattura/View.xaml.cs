using System.Threading;
using System.Windows;
using System.Windows.Input;
using FaPA.Infrastructure.Utils;

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

            Loaded += SetFocusOnFirstFocusableElement;
        }

        private void SetFocusOnFirstFocusableElement( object sender, RoutedEventArgs routedEventArgs )
        {
            ThreadPool.QueueUserWorkItem(
                               a =>
                               {
                                   Thread.Sleep( 100 );
                                   Query.Dispatcher.Invoke( () =>
                                   {
                                       if ( Query.IsEnabled ) Query.Focus();
                                   } );
                               } );
        }

        private void FattureGridSearch_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, FattureGridSearch );
        }
    }
}
