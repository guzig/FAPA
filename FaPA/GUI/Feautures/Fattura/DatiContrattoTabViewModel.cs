using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiContrattoTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiContrattoTabViewModel(IRepository repository, Core.Fattura instance) :
            base( ( Core.Fattura f ) => f.DatiContratto, repository, instance, "Contratto", true)
        { }

    }
}