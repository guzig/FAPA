using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using FaPA.Infrastructure;

namespace FaPA
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            // Ensure the current culture passed into bindings 
            // is the OS culture. By default, WPF uses en-US 
            // as the culture, regardless of the system settings.
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
              new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            BootStrapper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //AppDomain.CurrentDomain.SetThreadPrincipal(new CustomPrincipal());

            Presenters.Show("Main");

            //Presenters.Show("Anagrafica", new Action<GUI.Feautures.Anagrafica.Presenter>(p => p.CreateNewModel(0)));
            //Presenters.Show("Fattura", new Action<GUI.Feautures.Fattura.Presenter>(p => p.CreateNewModel(0)));

            //Presenters.Show("SearchAnagrafica");

            //Presenters.Show("SearchFattura");
        }
    }


}
