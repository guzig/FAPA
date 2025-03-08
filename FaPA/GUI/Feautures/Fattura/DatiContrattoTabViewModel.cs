using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiContrattoTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiContrattoTabViewModel(IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiContratto, repository, instance, "Contratto", true)
        { }

    }
}