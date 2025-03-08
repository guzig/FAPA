using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiIndirizzoViewModel : CrudViewModel<DatiTrasportoType, IndirizzoType>
    {
        //ctor
        public DatiIndirizzoViewModel( IRepository repository, DatiTrasportoType instance ) :
            base( repository, instance, f => f.IndirizzoResa, "Indirizzo", true )
        { }

        public override DatiTrasportoType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( WAFEL.Core.CORE.Fattura ) root ).DatiTrasporto;
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