using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiTerzoIntermediarioViewModel : EditWorkSpaceViewModel<Core.Fattura, TerzoIntermediarioSoggettoEmittenteType>
    {
        private SoggettoEmittenteType _soggettoEmittenteType;
        public SoggettoEmittenteType SoggettoEmittenteType
        {
            get { return _soggettoEmittenteType; }
            set
            {
                if ( value == _soggettoEmittenteType ) return;
                _soggettoEmittenteType = value;
                Instance.FatturaElettronicaHeader.SoggettoEmittente = _soggettoEmittenteType;
                NotifyOfPropertyChange( () => SoggettoEmittenteType );
            }
        }

        //ctor
        public DatiTerzoIntermediarioViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, f => f.TerzoIntermediarioOSoggettoEmittente, "Terzo emittente", false )
        {
            IsCloseable = false;
        }

        protected override void HookOnChanged( object poco )
        {
            var entity = poco as TerzoIntermediarioSoggettoEmittenteType;
            if ( entity == null ) return;

            HookChanged( entity );

            if ( entity.DatiAnagrafici == null ) return;

            HookChanged( entity );
            HookChanged( entity.DatiAnagrafici );
            HookChanged( entity.DatiAnagrafici.Anagrafica );
            HookChanged( entity.DatiAnagrafici.IdFiscaleIVA );

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