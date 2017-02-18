using System.Threading;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for Anagrafica.xaml
    /// </summary>
    public partial class Anagrafica 
    {
        public Anagrafica()
        {
            InitializeComponent();
        }

        protected override void SetFocusOnFirstFocusableElement()
        {
            ThreadPool.QueueUserWorkItem(
                               a =>
                               {
                                   Thread.Sleep( 100 );
                                   CodFiscale.Dispatcher.Invoke( () =>
                                   {
                                       if ( CodFiscale.IsEnabled ) CodFiscale.Focus();
                                   } );
                               } );
        }
    }
}
