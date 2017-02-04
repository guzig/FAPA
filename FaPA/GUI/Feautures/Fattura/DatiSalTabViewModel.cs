using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiSalTabViewModel : BaseTabsViewModel<DatiGeneraliType, DatiSALType[]>
    {
        //ctor
        public DatiSalTabViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiSAL, repository, instance, "Dati SAL", true)
        { }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void HookOnChanged( object poco )
        {
            var entity = poco as DatiGeneraliType;
            if ( entity == null ) return;

            HookChanged( entity );

            if ( entity.DatiSAL == null ) return;

            foreach ( var dettaglio in entity.DatiSAL )
            {
                HookChanged( dettaglio );
            }
        }

    }
}