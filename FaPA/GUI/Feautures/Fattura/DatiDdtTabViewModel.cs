using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiDdtTabViewModel : BaseTabsViewModel<Core.Fattura, DatiDDTType[]>
    {
        //ctor
        public DatiDdtTabViewModel(IRepository repository, Core.Fattura instance) :
            base((Core.Fattura f) => f.DatiDDT, repository, instance, "Dati DDT", true)
        { }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

    }
}