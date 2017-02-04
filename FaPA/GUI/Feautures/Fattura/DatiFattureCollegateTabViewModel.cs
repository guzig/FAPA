using FaPA.Core.FaPa;
using FaPA.Infrastructure;

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