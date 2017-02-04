using FaPA.Core.FaPa;
using FaPA.Infrastructure;

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