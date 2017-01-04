using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiOrdineTabViewModel : BaseTabsViewModel<Core.Fattura, DatiDocumentiCorrelatiType[]>
    {
        //ctor
        public DatiOrdineTabViewModel( IRepository repository, Core.Fattura instance ) :
            base( (Core.Fattura f) => f.DatiOrdineAcquisto, repository, instance, "Ordini", true )
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