using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiIndirizzoViewModel : EditWorkSpaceViewModel<DatiTrasportoType, IndirizzoType>
    {
        //ctor
        public DatiIndirizzoViewModel( IRepository repository, DatiTrasportoType instance ) :
            base( repository, instance, f => f.IndirizzoResa, "Indirizzo", true )
        { }

        public override DatiTrasportoType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).DatiTrasporto;
        }

        protected override object CreateInstance()
        {
            var instance = new IndirizzoType();

            Instance.IndirizzoResa = instance;

            return instance;
        }

        protected override bool CanAddEntity( object obj )
        {
            return Instance != null;
        }
    }
}