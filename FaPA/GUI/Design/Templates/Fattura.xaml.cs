using System;
using System.Windows;

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


        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            CrossCoupledPropsGrid.BindingGroup.CommitEdit();
        }
    }
}
