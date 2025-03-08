using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiDdtTabViewModel : CrudListViewModel<DatiGeneraliType, DatiDDTType[]>
    {
        //ctor
        public DatiDdtTabViewModel(IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiDDT, repository, instance, "Dati DDT", true)
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