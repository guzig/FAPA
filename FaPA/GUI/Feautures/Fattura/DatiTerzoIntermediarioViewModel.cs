using System.ComponentModel;
using FaPA.Core;
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
   

        public override void PersitEntity()
        {
            if ( CurrentPoco != null )
            {
                Instance.FatturaElettronicaHeader.SoggettoEmittente = 
                    ( ( TerzoIntermediarioSoggettoEmittenteType  ) CurrentPoco).SoggettoEmittente;
            }   
                   
            base.PersitEntity();
        }

        protected override void HookChanged( INotifyPropertyChanged poco )
        {
            var entity = poco as TerzoIntermediarioSoggettoEmittenteType;

            if ( entity?.DatiAnagrafici == null ) return;

            base.HookChanged( ( INotifyPropertyChanged ) entity );
            base.HookChanged( ( INotifyPropertyChanged ) entity.DatiAnagrafici );
            base.HookChanged( ( INotifyPropertyChanged ) entity.DatiAnagrafici.Anagrafica );
            base.HookChanged( ( INotifyPropertyChanged) entity.DatiAnagrafici.IdFiscaleIVA );

            ( ( INotifyPropertyChanged ) entity.DatiAnagrafici ).PropertyChanged += OnPropertyChanged;
            //( ( INotifyPropertyChanged ) entity.DatiAnagrafici.IdFiscaleIVA ).PropertyChanged += OnPropertyChanged;
            ( ( INotifyPropertyChanged ) entity.DatiAnagrafici.Anagrafica ).PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !( sender is BaseEntity ) ) return;

            var validatable = ( TerzoIntermediarioSoggettoEmittenteType ) CurrentPoco;

            validatable.Validate();

            //validatable.HandleValidationResults();

            if ( sender is DatiAnagraficiTerzoIntermediarioType )
                ( ( IValidatable ) validatable ).HandleValidationResults( "CodiceFiscale" );

            //if ( sender is IdFiscaleType )
            //    ( ( IValidatable ) validatable ).HandleValidationResults( "CurrentPoco.DatiAnagrafici.IdFiscaleIVA" );

            if ( sender is AnagraficaType )
                ( ( IValidatable ) validatable ).HandleValidationResults( "DatiAnagrafici.DatiAnagrafici.Anagrafica" );

        }

        protected override object CreateInstance()
        {
            var instance = new TerzoIntermediarioSoggettoEmittenteType()
            {
                DatiAnagrafici = new DatiAnagraficiTerzoIntermediarioType()
                {
                    //IdFiscaleIVA = new IdFiscaleType() ,
                    Anagrafica = new AnagraficaType() 
                }
            };

            Instance.TerzoIntermediarioOSoggettoEmittente = instance;

            return instance;
        }

    }
}