using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using FaPA.GUI.Controls.MyTabControl;

namespace FaPA.GUI.Utils
{
    public static class WpfHelpers
    {
        public static void MoveFocusToNextRowInGrid(DataGrid dataGrid)
        {
            dataGrid.Focus();
            if (dataGrid.Items.Count == 0)
                return;
            dataGrid.CommitEdit();
            int index = dataGrid.Items.Count - 1;
            dataGrid.SelectedItem = dataGrid.Items[index];
            dataGrid.ScrollIntoView(dataGrid.Items[index]);
            var dgrow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.Items[index]);
            dgrow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            dataGrid.BeginEdit();
        }

        public static void DataGridEditEnding(object sender, DataGridRowEditEndingEventArgs e, FrameworkElement crossCoupledProps)
        {
            var dataGrid = sender as DataGrid;
            if (e.EditAction != DataGridEditAction.Commit) return;

            var view = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource) as ListCollectionView;
            if (view.IsAddingNew || view.IsEditingItem)
            {
                dataGrid.Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
                {
                    // This callback will be called after the UserEntitiesView
                    // has pushed the changes back to the DataGrid.ItemSource.

                    // Write code here to save the data to the database.

                    //MoveFocusRowGrid(dataGrid);

                    crossCoupledProps?.BindingGroup?.CommitEdit();

                    return null;
                }), DispatcherPriority.Background, new object[] { null });
            }
        }

        public static void ShowErrorSavingEntityMsg()
        {
            const string msg = "L'operazione di salvataggio è fallita per un errore imprevisto!";
            var caption = "Errore irreversibile durante durante il salvataggio";
            MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        //public static void ShowErrorDeletingMsg()
        //{
        //    const string msg = "L'operazione di eliminazione è fallita per un errore imprevisto!";
        //    var caption = "Oggetto non eliminato";
        //    Execute.OnUIThread(() => Xceed.Wpf.Toolkit.MessageBox.Show(msg, caption, MessageBoxButton.OK,
        //           MessageBoxImage.Hand));
        //}

        public static void TabItem_OnPreviewMouseLeftButtonDown( object sender, RoutedEventArgs e, TabControl tabControl )
        {
            if ( !( sender is TabItem ) || !( tabControl.SelectedIndex > 0L ) ) return;

            tabControl.Focus();

            var currentItem = tabControl.ItemContainerGenerator.ContainerFromIndex( tabControl.SelectedIndex ) as TabItem;

            var targetItem = ( TabItem ) sender;

            if ( currentItem == null )
                return;

            var currentWorkSpace = currentItem.DataContext as WorkspaceViewModel;

            if ( currentWorkSpace == null )
                return;

            //se la scheda non è bloccata o il click è sulla stessa scheda di origine
            if ( string.IsNullOrWhiteSpace( currentWorkSpace.LockMessage ) || Equals( targetItem, currentItem ) )
                return;

            e.Handled = true;

            MessageBox.Show( currentWorkSpace.LockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation );
        }

    }
}

