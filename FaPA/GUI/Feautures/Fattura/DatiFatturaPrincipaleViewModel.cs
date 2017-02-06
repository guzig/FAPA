using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiFatturaPrincipaleViewModel : EditWorkSpaceViewModel<DatiGeneraliType, FatturaPrincipaleType>
    {
        //ctor
        public DatiFatturaPrincipaleViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( repository, instance, f => f.FatturaPrincipale, "Fattura principale", true )
        { }

        public override object Read()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).DatiGenerali;
        }

    }
}