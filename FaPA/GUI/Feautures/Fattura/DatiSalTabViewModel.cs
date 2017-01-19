using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiSalTabViewModel : BaseTabsViewModel<Core.Fattura, DatiSALType[]>
    {
        //ctor
        public DatiSalTabViewModel(IRepository repository, Core.Fattura instance) :
            base((Core.Fattura f) => f.DatiSAL, repository, instance, "Dati SAL", true)
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