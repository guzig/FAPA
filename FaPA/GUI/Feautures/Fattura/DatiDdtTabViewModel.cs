using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiDdtTabViewModel : BaseTabsViewModel<DatiGeneraliType, DatiDDTType[]>
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
            return ( ( Core.Fattura ) root ).DatiGenerali;
        }
    }
}