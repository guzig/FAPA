using System.Windows;
using System.Windows.Controls;
using FaPA.GUI.Utils;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for DatiGeneraliDocumento.xaml
    /// </summary>
    public partial class DatiDocumento : UserControl
    {
        public DatiDocumento()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonDown( object sender, RoutedEventArgs e )
        {
            WpfHelpers.TabItem_OnPreviewMouseLeftButtonDown( sender, e, TabControl );
        }




    }
}
