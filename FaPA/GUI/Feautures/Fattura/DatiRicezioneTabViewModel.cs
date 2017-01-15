using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiRicezioneTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiRicezioneTabViewModel( IRepository repository, Core.Fattura instance ) :
            base( ( Core.Fattura f ) => f.DatiRicezione, repository, instance, "Dati ricezione", true )
        { }

    }
}