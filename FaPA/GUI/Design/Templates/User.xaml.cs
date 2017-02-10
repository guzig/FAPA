using System.Threading;
using System.Windows.Controls;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User 
    {
        protected override void SetFocusOnFirstFocusableElement()
        {

            ThreadPool.QueueUserWorkItem(
                               a =>
                               {
                                   Thread.Sleep( 100 );
                                   UserName.Dispatcher.Invoke( () =>
                                   {
                                       if ( !Id.IsEnabled )
                                           UserName.Focus();
                                       else
                                           Id.Focus();
                                   } );
                               } );

        }
        public User()
        {
            InitializeComponent();
        }
    }
}
