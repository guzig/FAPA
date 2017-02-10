using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{

    public class DatiRappresentanteFiscaleViewModel : EditWorkSpaceViewModel<Core.Fattura, RappresentanteFiscaleType>
    {

        //ctor
        public DatiRappresentanteFiscaleViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, f => f.RappresentanteFiscale, "Rapp.Fiscale", false )
        {
            IsCloseable = false;
        }

        protected override void HookChanged( object poco )
        {
            var entity = poco as RappresentanteFiscaleType;
            if ( entity == null ) return;

            HookChanged( entity );

            if ( entity.DatiAnagrafici == null ) return;

            base.HookChanged( entity );
            base.HookChanged( entity.DatiAnagrafici );
            base.HookChanged( entity.DatiAnagrafici.Anagrafica );
            base.HookChanged( entity.DatiAnagrafici.IdFiscaleIVA );

        }

        protected override object CreateInstance()
        {
            var instance = new RappresentanteFiscaleType()
            {
                DatiAnagrafici = new DatiAnagraficiRappresentanteType()
                {
                    IdFiscaleIVA = new IdFiscaleType(),
                    Anagrafica = new AnagraficaType()
                }
            };
            
            Instance.RappresentanteFiscale = instance;

            return instance;
        }

    }
}