using System;
using System.Threading;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for Fattura.xaml
    /// </summary>
    public partial class Fattura
    {
        public Fattura()
        {
            InitializeComponent();

        }

        private void OnCommitBindingGroup( object sender, EventArgs e)
        {
            CrossCoupledPropsGrid.BindingGroup.CommitEdit();
        }

        protected override void SetFocusOnFirstFocusableElement()
        {
            ThreadPool.QueueUserWorkItem(
                               a =>
                               {
                                   Thread.Sleep( 100 );
                                   Fornitore.Dispatcher.Invoke( () =>
                                   {
                                       if ( Fornitore.IsEnabled ) Fornitore.Focus();
                                   } );
                               } );
        }



    }
}
