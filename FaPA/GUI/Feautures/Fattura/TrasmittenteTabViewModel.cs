using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class TrasmittenteTabViewModel : CrudViewModel<WAFEL.Core.CORE.Fattura, DatiTrasmissioneType>
    {

        //ctor
        public TrasmittenteTabViewModel( IRepository repository, WAFEL.Core.CORE.Fattura instance ) :
            base( repository, instance, f => f.DatiTrasmissione, "Trasmittente", false )
        {
            IsCloseable = false;
        }

    }
}