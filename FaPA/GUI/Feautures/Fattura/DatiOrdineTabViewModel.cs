using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

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