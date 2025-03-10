using System.ComponentModel;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{

    public class DatiRappresentanteFiscaleViewModel : CrudViewModel<Core.Fattura, RappresentanteFiscaleType>
    {

        //ctor
        public DatiRappresentanteFiscaleViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, f => f.RappresentanteFiscale, "Rapp.Fiscale", false )
        {
            IsCloseable = false;
        }

        protected override void HookChanged( INotifyPropertyChanged poco )
        {
            var entity = poco as RappresentanteFiscaleType;
            if ( entity == null ) return;

            base.HookChanged( ( INotifyPropertyChanged ) entity );

            if ( entity.DatiAnagrafici == null ) return;

            base.HookChanged( ( INotifyPropertyChanged ) entity.DatiAnagrafici );
            base.HookChanged( ( INotifyPropertyChanged ) entity.DatiAnagrafici.Anagrafica );
            base.HookChanged( ( INotifyPropertyChanged ) entity.DatiAnagrafici.IdFiscaleIVA );

        }


        protected override object CreateInstance()
        {
            var instance = new RappresentanteFiscaleType()
            {
                DatiAnagrafici = new DatiAnagraficiRappresentanteType()
                {
                    IdFiscaleIVA = new IdFiscaleType() {IdPaese = "IT"},
                    Anagrafica = new AnagraficaType()
                }
            };
            
            Instance.RappresentanteFiscale = instance;

            return instance;
        }

    }
}