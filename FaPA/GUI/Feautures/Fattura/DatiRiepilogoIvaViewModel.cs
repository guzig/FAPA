using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiRiepilogoIvaViewModel : CrudListViewModel<DatiBeniServiziType, DatiRiepilogoType[]>
    {
        //ctor
        public DatiRiepilogoIvaViewModel(IRepository repository, DatiBeniServiziType instance) :
            base( f => f.DatiRiepilogo, repository, instance, "Riepilogo IVA", false)
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


        public override DatiBeniServiziType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( WAFEL.Core.CORE.Fattura )root ).DatiBeniServizi;
        }

    }
}