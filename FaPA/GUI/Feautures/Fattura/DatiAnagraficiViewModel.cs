using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiAnagraficiViewModel : EditWorkSpaceViewModel<DatiTrasportoType, DatiAnagraficiVettoreType>
    {
        //ctor
        public DatiAnagraficiViewModel( IRepository repository, DatiTrasportoType instance ) :
            base( repository, instance, f => f.DatiAnagraficiVettore, "Anagrafici", true )
        {
        }

        public override DatiTrasportoType ReadInstance()
        {
           var root = Repository.Read();
           return ( ( Core.Fattura ) root ).DatiTrasporto;
        }

        protected override object CreateInstance()
        {
            var instance = new DatiAnagraficiVettoreType()
            {
                IdFiscaleIVA = new IdFiscaleType(),
                Anagrafica = new AnagraficaType()
            };

            Instance.DatiAnagraficiVettore = instance;

            return instance;
        }

        protected override void HookOnChanged( object poco )
        {
            var entity = poco as DatiAnagraficiVettoreType;
            if ( entity == null ) return;

            HookChanged( entity );
            HookChanged( entity.Anagrafica );
            HookChanged( entity.IdFiscaleIVA );
        }
    }
}