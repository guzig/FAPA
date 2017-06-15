using System;
using System.Windows;
using System.Windows.Controls;
using FaPA.Infrastructure.Finder;
using FaPA.Infrastructure.Utils;
using EnumerationExtension = FaPA.Infrastructure.Utils.EnumerationExtension;

namespace FaPA.GUI.Design.Templates.SearchTypeTemplates
{
    /// <summary>
    /// Logica di interazione per TypeNumericsCriterion.xaml
    /// </summary>
    public partial class TypeNullDoubleCriterion : UserControl
    {
        public TypeNullDoubleCriterion()
        {
            InitializeComponent();
        }


        private void OnCommitBindingGroup(object sender, EventArgs e)
        {
            CrossCoupledProps.BindingGroup.CommitEdit();
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

        private void TextError(object sender, ValidationErrorEventArgs e)
        {
            var prop = DataContext as DoubleSearchProperty;
            prop.RootFinder.IsValid = false;
        }

    }
}
