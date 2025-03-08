using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiSalTabViewModel : CrudListViewModel<DatiGeneraliType, DatiSALType[]>
    {
        //ctor
        public DatiSalTabViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiSAL, repository, instance, "Dati SAL", true)
        { }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        public override DatiGeneraliType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( WAFEL.Core.CORE.Fattura ) root ).DatiGenerali;
        }

    }
}