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
            
            Hook( entity );

            if ( entity.DettaglioPagamento == null ) return;

            foreach ( var dettaglio in entity.DettaglioPagamento )
            {
                Hook(dettaglio);
            }
        }

        private void Hook(object poco)
        {
            var notifyPropertyChanged = poco as INotifyPropertyChanged;
            if (notifyPropertyChanged == null) return;
            notifyPropertyChanged.PropertyChanged -= OnPropChanged;
            notifyPropertyChanged.PropertyChanged += OnPropChanged;
        }
    }
}