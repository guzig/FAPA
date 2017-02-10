using FaPA.GUI.Utils;

namespace FaPA.GUI.Feautures.User
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class View : WindowBase
    {
        public Presenter Presenter { get; set; }
        public View()
        {
            InitializeComponent();
        }
    }
}
