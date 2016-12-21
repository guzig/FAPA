using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiConvenzioneTabViewModel : BaseTabsViewModel<Core.Fattura, DatiDocumentiCorrelatiType[]>
    {
        //ctor
        public DatiConvenzioneTabViewModel(IRepository repository, Core.Fattura instance) :
            base((Core.Fattura f) => f.DatiConvenzione, repository, instance, "Convenzione", true)
        { }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void OnRequestClose()
        {
            CloseIfNotEmpty();
        }

    }
}