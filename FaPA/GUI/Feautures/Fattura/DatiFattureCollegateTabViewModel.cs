using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiFattureCollegateTabViewModel : BaseDatiCorrelatiViewModel
    {
        //ctor
        public DatiFattureCollegateTabViewModel( IRepository repository, Core.Fattura instance ) :
            base( ( Core.Fattura f ) => f.DatiFattureCollegate, repository, instance, "Fatture collegate", true )
        { }

    }
}