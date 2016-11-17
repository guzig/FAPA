using System;
using System.Windows;
using System.Windows.Controls;
using FaPA.GUI.Utils;
using FaPA.Infrastructure.Finder;
using EnumerationExtension = FaPA.GUI.Utils.EnumerationExtension;

namespace FaPA.GUI.Design.Templates.SearchTypeTemplates
{
    /// <summary>
    /// Logica di interazione per UserControl1.xaml
    /// </summary>
    public partial class TypeNullInt64Criterion : UserControl
    {
        public TypeNullInt64Criterion()
        {
            InitializeComponent();
        }

        private void OnCommitBindingGroup(object sender, EventArgs e)
        {
            if (CrossCoupledProps.BindingGroup.HasValidationError)
                CrossCoupledProps.BindingGroup.CancelEdit();
            else
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
            CrossCoupledProps.BindingGroup.CancelEdit();

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
