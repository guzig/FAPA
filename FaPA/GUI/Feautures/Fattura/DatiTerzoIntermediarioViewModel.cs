using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiTerzoIntermediarioViewModel : EditWorkSpaceViewModel<Core.Fattura, TerzoIntermediarioSoggettoEmittenteType>
    {
        //ctor
        public DatiTerzoIntermediarioViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, f => f.TerzoIntermediarioOSoggettoEmittente, "Terzo emittente", false )
        {
            IsCloseable = false;
        }

        protected override void PersitEntity()
        {
            Instance.FatturaElettronicaHeader.SoggettoEmittente = 
                ( ( TerzoIntermediarioSoggettoEmittenteType  ) CurrentPoco).SoggettoEmittente;
          
            base.PersitEntity();
        }

        protected override void HookChanged( object poco )
        {
            var entity = poco as TerzoIntermediarioSoggettoEmittenteType;

            if ( entity?.DatiAnagrafici == null ) return;

            base.HookChanged( entity );
            base.HookChanged( entity.DatiAnagrafici );
            base.HookChanged( entity.DatiAnagrafici.Anagrafica );
            base.HookChanged( entity.DatiAnagrafici.IdFiscaleIVA );
        }

        protected override object CreateInstance()
        {
            var instance = new TerzoIntermediarioSoggettoEmittenteType()
            {
                DatiAnagrafici = new DatiAnagraficiTerzoIntermediarioType()
                {
                    IdFiscaleIVA = new IdFiscaleType(),
                    Anagrafica = new AnagraficaType()
                }
            };

            Instance.TerzoIntermediarioOSoggettoEmittente = instance;

            return instance;
        }

    }
}