using System.Windows;

namespace FaPA.GUI.Feautures.Anagrafica
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        public Presenter Presenter { get; set; }

        public View()
        {
            InitializeComponent();
        }
    }
}
