using System.Windows;
using System.Windows.Input;

namespace FaPA.GUI.Feautures.LogInManager
{
    public partial class View : Window
    {
		public View()
		{
			InitializeComponent();
        }

        private void PART_TITLEBAR_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            DragMove();
        }

        private void PART_CLOSE_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void PART_MAXIMIZE_RESTORE_Click( object sender, RoutedEventArgs e )
        {
            if ( this.WindowState == System.Windows.WindowState.Normal )
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void PART_MINIMIZE_Click( object sender, RoutedEventArgs e )
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
