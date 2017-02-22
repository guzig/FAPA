using System;
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
            base(f => f.DatiPagamento, repository, instance, "Pagamenti", false)
        {
            CurrentEntityPropChanged += Kl;
        }

        private void Kl(object sender, PropertyChangedEventArgs eventarg)
        {
            var dettaglioPagamentoType = Instance.DatiPagamento[0].DettaglioPagamento[0];
            var fff0 = ReferenceEquals(dettaglioPagamentoType, sender);
        }

        protected override void OnCurrentChanged(object sender, EventArgs eventArgs)
        {
            var dettaglioPagamentoType = Instance.DatiPagamento[0].DettaglioPagamento[0];
            var fff0 = ReferenceEquals(dettaglioPagamentoType, sender);

            base.OnCurrentChanged(sender, eventArgs);
            //ImportoPagamento = UserProperty[0].DettaglioPagamento[0].ImportoPagamento;
        }

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

        protected override void HookChanged(object poco)
        {
            var entity = poco as DatiPagamentoType;
            if ( entity == null ) return;

            base.HookChanged( entity );

            if ( entity.DettaglioPagamento == null ) return;

            foreach ( var dettaglio in entity.DettaglioPagamento )
            {
                base.HookChanged(dettaglio);
            }
        }

    }
}