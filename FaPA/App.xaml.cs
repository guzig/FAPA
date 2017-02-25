using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using FaPA.AppServices;
using FaPA.DomainServices.AuthenticationServices;
using FaPA.GUI.Utils;
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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo( "it-IT" );
            FrameworkElement.LanguageProperty.OverrideMetadata( typeof( FrameworkElement ), new FrameworkPropertyMetadata(
                        XmlLanguage.GetLanguage( CultureInfo.CurrentCulture.IetfLanguageTag ) ) );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if ( StoreAccess.IsServerConnected() )
                BootStrapper.Initialize();
            else
            {
                const string caption = "Fattura PA: servizio database Sql Server non raggiungibile";
                string msg = "Il servizio di database di MS Sql Server, sull'istanza PAWARE, non è stato rilevato. " + Environment.NewLine +
                             "L'applicazione verrà terminata.";
                Xceed.Wpf.Toolkit.MessageBox.Show( msg, caption, MessageBoxButton.OK, MessageBoxImage.Hand );
                Application.Current.Shutdown();
            }

            ShowCursor.Show();
            base.OnStartup( e );

            //AppDomain.CurrentDomain.SetThreadPrincipal( new CustomPrincipal() );
            //Presenters.Show( "Main" );


            //Presenters.Show("Anagrafica", new Action<GUI.Feautures.Anagrafica.Presenter>(p => p.CreateNewModel(0)));
            Presenters.Show("Fattura", new Action<GUI.Feautures.Fattura.Presenter>(p => p.CreateNewModel(0)));

            //Presenters.Show("SearchAnagrafica");

            //Presenters.Show("SearchFattura");
        }
    }


}
