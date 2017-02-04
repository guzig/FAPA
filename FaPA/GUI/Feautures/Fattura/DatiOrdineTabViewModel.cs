using FaPA.Core.FaPa;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiOrdineTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiOrdineTabViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiOrdineAcquisto, repository, instance, "Dati ordini acquisto", true )
        { }

        //protected override void OnRequestClose()
        //{
        //    CloseIfNotEmpty();
        //}
    }
}