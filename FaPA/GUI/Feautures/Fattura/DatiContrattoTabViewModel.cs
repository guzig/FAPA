using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiContrattoTabViewModel : BaseTabsViewModel<Core.Fattura, DatiDocumentiCorrelatiType[]>
    {
        //ctor
        public DatiContrattoTabViewModel(IRepository repository, Core.Fattura instance) :
            base( ( Core.Fattura f ) => f.DatiContratto, repository, instance, "Contratto", true)
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