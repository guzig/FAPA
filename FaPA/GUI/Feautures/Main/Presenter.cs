using System.Threading;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Feautures.Main
{
    class Presenter: AbstractPresenter<Model, View>
    {
        public Presenter()
        {
            Presenters.ShowDialog( "LogIn" );

            if ( !Thread.CurrentPrincipal.Identity.IsAuthenticated )
            {
                System.Windows.Application.Current.Shutdown();
            }

            ShowCursor.Show();
        }
    }
}
