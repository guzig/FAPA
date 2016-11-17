using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using FaPA.Infrastructure.Finder;

namespace FaPA.GUI.Design.Templates.SearchTypeTemplates
{
    /// <summary>
    /// Logica di interazione per TypeDateTimeCriterion.xaml
    /// </summary>
    public partial class TypeDateTimeCriterion : UserControl
    {
        public TypeDateTimeCriterion()
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

        private void CxSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var dt = (DateTimeSearchProperty)DataContext;
            //var temp = new ObservableCollection<ItemValue<DateTime>>();

            if (dt == null) return;

            //dt.OperatorValues.Clear();

            if (e.AddedItems.Count <= 0) return;

            foreach (DateTime date in e.AddedItems)
            {
                dt.OperatorValues.Add(new ItemValue<DateTime?> { Item = date });
            }

            if (e.RemovedItems.Count >= 0)
            {
                var temp = new ObservableCollection<ItemValue<DateTime?>>();
                foreach (var operatorValue in dt.OperatorValues.
                    Where(operatorValue => !e.RemovedItems.Contains(operatorValue.Item)))
                {
                    temp.Add(operatorValue);
                }
                dt.OperatorValues = temp;
            }
            
            CrossCoupledProps.BindingGroup.CommitEdit();

        }

        private void DatePicker_Error(object sender, ValidationErrorEventArgs e)
        {
            var prop = DataContext as DateTimeSearchProperty;
            //prop.OperatorValue = null;
            prop.RootFinder.IsValid = false;
        }


    }
}
