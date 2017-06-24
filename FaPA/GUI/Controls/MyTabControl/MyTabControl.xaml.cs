using System.Windows;
using System.Windows.Controls;

namespace FaPA.GUI.Controls.MyTabControl
{
    /// <summary>
    /// Logica di interazione per MyTabControl.xaml
    /// </summary>
    public partial class MyTabControl : UserControl
    {
        public MyTabControl()
        {
            InitializeComponent();
        }
        
        private void TabControlPreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (!(sender is TabItem) || !(tabControl.SelectedIndex>0L)) return;

            tabControl.Focus();
            
            var currentItem = tabControl.ItemContainerGenerator.ContainerFromIndex(tabControl.SelectedIndex) as TabItem;

            var targetItem = sender as TabItem;

            if (currentItem == null)
                return;

            var currentWorkSpace = currentItem.Content as WorkspaceViewModel;

            if (currentWorkSpace == null)
                return;

            //se la scheda non è bloccata o il click è sulla stessa scheda di origine
            if (string.IsNullOrWhiteSpace(currentWorkSpace.LockMessage) || Equals(targetItem, currentItem)) 
                return;
            
            e.Handled = true;
            
            Xceed.Wpf.Toolkit.MessageBox.Show(currentWorkSpace.LockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            
            //var ex = e.OriginalSource as FrameworkElement;
            //DependencyObject focusScope = FocusManager.GetFocusScope(this);

            //var v = focusScope as Window;

            //FocusManager.SetFocusedElement(focusScope, ex);

        }
    }
}
