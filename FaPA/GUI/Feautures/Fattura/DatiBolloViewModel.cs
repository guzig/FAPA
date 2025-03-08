using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiBolloViewModel : CrudViewModel<DatiGeneraliDocumentoType, DatiBolloType>
    {
        //ctor
        public DatiBolloViewModel( IRepository repository, DatiGeneraliDocumentoType instance ) :
            base( repository, instance, f => f.DatiBollo, "Bollo", true )
        { }

        public override DatiGeneraliDocumentoType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( WAFEL.Core.CORE.Fattura ) root ).DatiGeneraliDocumento;
        }

    }
}