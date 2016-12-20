using System;
using System.Windows;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class RitenutaTabViewModel : EditWorkSpaceViewModel<Core.Fattura, DatiRitenutaType>
    {

        //ctor
        public RitenutaTabViewModel(IRepository repository, Core.Fattura instance ) : 
            base( repository, instance, (Core.Fattura f) => f.Ritenuta,"Ritenuta", true )
        {}

        //protected override void OnCancelDelegateExecute()
        //{
        //    base.OnCancelDelegateExecute();
        //    //if ( UserProperty != null ) return;
        //    //EventPublisher.Publish( new RemoveTabView( this, ParentViewModel ), this );
        //}


        protected override void OnRequestClose()
        {
            if (UserProperty != null)
            {
                const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
                MessageBox.Show(lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            base.OnRequestClose();
        }

    }
}