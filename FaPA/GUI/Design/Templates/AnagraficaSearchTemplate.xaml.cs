using System.Windows;
using System.Windows.Controls;
using FaPA.Infrastructure.Finder;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Interaction logic for LiquidazioneSearchTemplate.xaml
    /// </summary>
    public partial class AnagraficaSearchTemplate : UserControl
    {
        #region - Dependency Properties -
        public static readonly DependencyProperty AnagraficaFinderProperty =
            DependencyProperty.Register("AnagraficaFinder", typeof(ObjectFinder),
               typeof(AnagraficaSearchTemplate), new FrameworkPropertyMetadata(null, null));

        public ObjectFinder AnagraficaFinder
        {
            get { return (ObjectFinder)GetValue(AnagraficaFinderProperty); }
            set { SetValue(AnagraficaFinderProperty, value); }
        }

        #endregion
        public AnagraficaSearchTemplate()
        {
            InitializeComponent();
        }
    }
}
