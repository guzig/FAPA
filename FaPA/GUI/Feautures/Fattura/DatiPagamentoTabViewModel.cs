using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using System.ComponentModel;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiPagamentoTabViewModel : CrudListViewModel<WAFEL.Core.CORE.Fattura, DatiPagamentoType[]>
    {
        //ctor
        public DatiPagamentoTabViewModel(IRepository repository, WAFEL.Core.CORE.Fattura instance) :
            base(f => f.DatiPagamento, repository, instance, "Pagamenti", false)
        {}

        protected override object CreateInstance()
        {
            var instance = new DatiPagamentoType()
            {
                DettaglioPagamento = new [] { new DettaglioPagamentoType() } 
            };
            return instance;
        }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void HookChanged( INotifyPropertyChanged poco )
        {
            var entity = poco as DatiPagamentoType;
            if ( entity == null ) return;

            base.HookChanged( ( INotifyPropertyChanged) entity );

            if ( entity.DettaglioPagamento == null ) return;

            foreach ( var dettaglio in entity.DettaglioPagamento )
            {
                base.HookChanged( ( INotifyPropertyChanged) dettaglio );
            }
        }

    }
}