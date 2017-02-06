using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiCassaPrevViewModel : EditWorkSpaceViewModel<DatiGeneraliDocumentoType, DatiCassaPrevidenzialeType>
    {
        //ctor
        public DatiCassaPrevViewModel( IRepository repository, DatiGeneraliDocumentoType instance ) :
            base( repository, instance, f => f.DatiCassaPrevidenziale, "Cassa prev.", true )
        { }

        public override DatiGeneraliDocumentoType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).DatiGeneraliDocumento;
        }

    }
}