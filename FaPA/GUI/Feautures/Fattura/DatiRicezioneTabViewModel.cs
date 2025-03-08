using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiRicezioneTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiRicezioneTabViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiRicezione, repository, instance, "Dati ricezione", true )
        { }

    }
}