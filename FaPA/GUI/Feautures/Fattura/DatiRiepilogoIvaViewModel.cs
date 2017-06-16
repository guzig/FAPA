using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

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
            return ( ( Core.Fattura )root ).DatiBeniServizi;
        }

    }
}