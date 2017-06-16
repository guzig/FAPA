using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class TrasmittenteTabViewModel : CrudViewModel<Core.Fattura, DatiTrasmissioneType>
    {

        //ctor
        public TrasmittenteTabViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, f => f.DatiTrasmissione, "Trasmittente", false )
        {
            IsCloseable = false;
        }

    }
}