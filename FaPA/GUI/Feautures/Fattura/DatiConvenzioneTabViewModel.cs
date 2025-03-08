using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

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