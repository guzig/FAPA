using System.Windows;
using System.Windows.Controls;
using FaPA.Infrastructure.Finder;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for FatturaSearchTemplate.xaml
    /// </summary>
    public partial class FatturaSearchTemplate : UserControl
    {
        #region - Dependency Properties -
        public static readonly DependencyProperty FatturaFinderProperty =
            DependencyProperty.Register("FatturaFinder", typeof(ObjectFinder),
               typeof(FatturaSearchTemplate), new FrameworkPropertyMetadata(null, null));

        public ObjectFinder FatturaFinder
        {
            get { return (ObjectFinder)GetValue(FatturaFinderProperty); }
            set { SetValue(FatturaFinderProperty, value); }
        }

        #endregion

        public FatturaSearchTemplate()
        {
            InitializeComponent();
        }

    }
}
