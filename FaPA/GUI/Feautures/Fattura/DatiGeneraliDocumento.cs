using System.Windows;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliDocumentoViewModel : EditWorkSpaceViewModel<Core.Fattura, DatiGeneraliDocumentoType>
    {
        //ctor
        public DatiGeneraliDocumentoViewModel(IRepository repository, Core.Fattura instance) : 
            base( repository, instance, (Core.Fattura f) => f.DatiGeneraliDocumento,"DatiGenerali", true )
        { }

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