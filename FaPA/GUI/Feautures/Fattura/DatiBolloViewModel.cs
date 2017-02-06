using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiBolloViewModel : EditWorkSpaceViewModel<DatiGeneraliDocumentoType, DatiBolloType>
    {
        //ctor
        public DatiBolloViewModel( IRepository repository, DatiGeneraliDocumentoType instance ) :
            base( repository, instance, f => f.DatiBollo, "Bollo", true )
        { }

        public override object Read()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).DatiGeneraliDocumento;
        }

    }
}