using FaPA.Core.FaPa;
using FaPA.Infrastructure;

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