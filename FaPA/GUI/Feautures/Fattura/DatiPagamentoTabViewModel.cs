using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiPagamentoTabViewModel : BaseTabsViewModel<Core.Fattura, DatiPagamentoType[]>
    {
        //ctor
        public DatiPagamentoTabViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, (Core.Fattura f) => f.DatiPagamento, "Pagamenti", false)
        {}

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