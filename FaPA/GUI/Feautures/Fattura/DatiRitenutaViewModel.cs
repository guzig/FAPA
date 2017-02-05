using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiRitenutaViewModel : EditWorkSpaceViewModel<DatiGeneraliDocumentoType, DatiRitenutaType>
    {
        //ctor
        public DatiRitenutaViewModel( IRepository repository, DatiGeneraliDocumentoType instance ) :
            base( repository, instance, f => f.DatiRitenuta, "Ritenuta", true )
        {}

        public override object Read()
        {
            var root = Repository.Read();
            Instance = ( ( Core.Fattura ) root ).DatiGeneraliDocumento;
            var userProp = GetterProp( Instance );
            return userProp;
        }
        
        //protected override void OnRequestClose()
        //{
        //    if (UserProperty != null)
        //    {
        //        const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
        //        MessageBox.Show(lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //        return;
        //    }
        //    base.OnRequestClose();
        //}

    }
}