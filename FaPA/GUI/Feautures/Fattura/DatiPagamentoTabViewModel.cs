using System.ComponentModel;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiPagamentoTabViewModel : BaseTabsViewModel<Core.Fattura, DatiPagamentoType[]>
    {
        //ctor
        public DatiPagamentoTabViewModel(IRepository repository, Core.Fattura instance) :
            base( (Core.Fattura f) => f.DatiPagamento, repository, instance, "Pagamenti", false )
        {
        }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void HookOnChanged(object poco)
        {
            var entity = poco as DatiPagamentoType;
            if ( entity == null ) return;
            
            HookChanged( entity );

            if ( entity.DettaglioPagamento == null ) return;

            foreach ( var dettaglio in entity.DettaglioPagamento )
            {
                HookChanged(dettaglio);
            }
        }

    }
}