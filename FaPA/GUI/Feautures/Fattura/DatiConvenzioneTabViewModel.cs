using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiConvenzioneTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiConvenzioneTabViewModel(IRepository repository, Core.Fattura instance) :
            base((Core.Fattura f) => f.DatiConvenzione, repository, instance, "Convenzione", true)
        { }

    }
}