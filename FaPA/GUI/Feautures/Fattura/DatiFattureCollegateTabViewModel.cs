using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiFattureCollegateTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiFattureCollegateTabViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiFattureCollegate, repository, instance, "Fatture collegate", true )
        { }

    }
}