using System.Windows;
using System.Windows.Controls;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Design.Templates.SearchTypeTemplates
{
    /// <summary>
    /// Interaction logic for TypeAssociationCriterion.xaml
    /// </summary>
    public partial class TypeTagsAssociationByCheckBoxCriterion : UserControl
    {

        public TypeTagsAssociationByCheckBoxCriterion()
        {
            InitializeComponent();
        }

        private void ListValuesSearchControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CrossCoupledProps.BindingGroup.CommitEdit();
        }

        private DataGrid _dataGrid;

        private void DataGridLoaded(object sender, RoutedEventArgs e)
        {
            _dataGrid = sender as DataGrid;

            MoveFocusRowGrid(_dataGrid);

        }

        private static void MoveFocusRowGrid(DataGrid dataGrid)
        {
            WpfHelpers.MoveFocusToNextRowInGrid(dataGrid);
        }

        private void OperatorComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CrossCoupledProps.BindingGroup.CommitEdit();

            if (e.AddedItems.Count <= 0) return;

            var operatorType = (EnumerationExtension.EnumerationMember)e.AddedItems[0];
            var val = operatorType.Value.ToString();
            if (val != "OneOf" && val != "NoneOf") return;

            if (_dataGrid == null) return;

            MoveFocusRowGrid(_dataGrid);
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            CrossCoupledProps.BindingGroup.CommitEdit();
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            CrossCoupledProps.BindingGroup.CommitEdit(); ;
        }
    }
}
