using System.Windows;
using System.Windows.Controls;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Design.Templates.SearchTypeTemplates
{
    /// <summary>
    /// Interaction logic for TypeAssociationCriterion.xaml
    /// </summary>
    public partial class TypeClassificazioneCriterion : UserControl
    {

        public TypeClassificazioneCriterion()
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
            WpfHelpers.MoveFocusToNextRowInGrid( dataGrid );
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CrossCoupledProps.BindingGroup.CommitEdit();

            if (e.AddedItems.Count <= 0) return;

            var operatorType = (EnumerationExtension.EnumerationMember)e.AddedItems[0];
            var val = operatorType.Value.ToString();
            if (val != "OneOf" && val != "NoneOf") return;

            if (_dataGrid == null) return;

            MoveFocusRowGrid(_dataGrid);
        }

    }
}
