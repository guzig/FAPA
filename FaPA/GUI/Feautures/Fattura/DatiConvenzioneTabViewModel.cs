using FaPA.Core.FaPa;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiConvenzioneTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiConvenzioneTabViewModel(IRepository repository, DatiGeneraliType instance ) :
            base(f => f.DatiConvenzione, repository, instance, "Convenzione", true)
        { }

    }
}