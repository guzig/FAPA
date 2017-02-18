using System.Threading;
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

        private void AnagraficaGridSearch_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridHelpers.DataGridKeyUpEventHandler( e, AnagraficaGridSearch );
        }
    }
}
