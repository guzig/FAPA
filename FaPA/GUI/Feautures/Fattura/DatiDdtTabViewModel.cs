using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiDdtTabViewModel : BaseTabsViewModel<DatiGeneraliType, DatiDDTType[]>
    {
        //ctor
        public DatiDdtTabViewModel(IRepository repository, DatiGeneraliType instance ) :
            base( f => f.DatiDDT, repository, instance, "Dati DDT", true)
        { }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void HookChanged(object poco )
        {
            var entity = poco as DatiGeneraliType;
            if ( entity == null ) return;

            HookChanged( entity );

            if ( entity.DatiDDT == null ) return;

            foreach ( var dettaglio in entity.DatiDDT )
            {
                HookChanged( dettaglio );
            }
        }

        public override DatiGeneraliType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).DatiGenerali;
        }
    }
}