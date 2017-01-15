using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using FaPA.Infrastructure;
using NHibernate.Proxy.DynamicProxy;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliViewModel : EditWorkSpaceViewModel<Core.Fattura, DatiGeneraliType>
    {
        #region fields

        private DatiOrdineTabViewModel _datiOrdini;
        private DatiContrattoTabViewModel _datiContratto;
        private DatiConvenzioneTabViewModel _datiConvenzione;
        private DatiFattureCollegateTabViewModel _datiFattureCollegate;
        private DatiRicezioneTabViewModel _datiRicezione;

        #endregion

        #region props

        public DatiConvenzioneTabViewModel DatiConvenzione
        {
            get { return _datiConvenzione; }
            set
            {
                if ( Equals( value, _datiConvenzione ) ) return;
                _datiConvenzione = value;
                NotifyOfPropertyChange( () => DatiConvenzione );
            }
        }

        public DatiContrattoTabViewModel DatiContratto
        {
            get { return _datiContratto; }
            set
            {
                if ( Equals( value, _datiContratto ) ) return;
                _datiContratto = value;
                NotifyOfPropertyChange( () => DatiContratto );
            }
        }

        public DatiOrdineTabViewModel DatiOrdini
        {
            get { return _datiOrdini; }
            set
            {
                if ( Equals( value, _datiOrdini ) ) return;
                _datiOrdini = value;
                NotifyOfPropertyChange( () => DatiOrdini );
            }
        }

        public DatiRicezioneTabViewModel DatiRicezione
        {
            get { return _datiRicezione; }
            set
            {
                if ( Equals( value, _datiRicezione ) ) return;
                _datiRicezione = value;
                NotifyOfPropertyChange( () => DatiRicezione );
            }
        }

        public DatiFattureCollegateTabViewModel DatiFattureCollegate
        {
            get { return _datiFattureCollegate; }
            set
            {
                if ( Equals( value, _datiFattureCollegate ) ) return;
                _datiFattureCollegate = value;
                NotifyOfPropertyChange( () => DatiFattureCollegate );
            }
        }
        #endregion

        #region Commands

        private ICommand _addTrasportoCommand;

        public ICommand AddTrasportoCommand
        {
            get
            {
                if ( _addTrasportoCommand != null ) return _addTrasportoCommand;
                _addTrasportoCommand = new RelayCommand( param => AddDatiTrasporto(),
                    ( e ) => Instance != null && Instance.DatiGenerali.DatiTrasporto == null );
                return _addTrasportoCommand;
            }
        }

        private void AddDatiTrasporto()
        {
            var current = CurrentPoco as DatiGeneraliType;
            if ( current == null ) return;

            object instance = new DatiTrasportoType();
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            ( ( IValidatable ) instance ).Validate();

            ObjectExplorer.TryProxiedAllInstances<BaseEntity>( ref instance, "FaPA.Core" );

            current.DatiTrasporto = ( DatiTrasportoType ) instance;

            HookChanged( instance );
        }

        private ICommand _addVettoreCommand;

        public ICommand AddVettoreCommand
        {
            get
            {
                if ( _addVettoreCommand != null ) return _addVettoreCommand;
                _addVettoreCommand = new RelayCommand( param => AddDatiVettore(),
                    ( e ) => Instance?.DatiGenerali.DatiTrasporto != null &&
                             Instance.DatiGenerali.DatiTrasporto.DatiAnagraficiVettore == null );
                return _addVettoreCommand;
            }
        }

        private void AddDatiVettore()
        {
            var current = CurrentPoco as DatiGeneraliType;
            if ( current == null ) return;

            object instance = new DatiAnagraficiVettoreType();
            ( ( DatiAnagraficiVettoreType ) instance ).Anagrafica = new AnagraficaType();
            ( ( DatiAnagraficiVettoreType ) instance ).IdFiscaleIVA = new IdFiscaleType();
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            ( ( IValidatable ) instance ).Validate();

            ObjectExplorer.TryProxiedAllInstances<BaseEntity>( ref instance, "FaPA.Core" );

            current.DatiTrasporto.DatiAnagraficiVettore = ( DatiAnagraficiVettoreType ) instance;

            HookChanged( instance );

            HookChanged( ( ( DatiAnagraficiVettoreType ) instance ).IdFiscaleIVA );
            HookChanged( ( ( DatiAnagraficiVettoreType ) instance ).Anagrafica );

            var anagraficaType = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto.DatiAnagraficiVettore.Anagrafica;
            var anagraficaType1 = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto.DatiAnagraficiVettore.IdFiscaleIVA;
            anagraficaType.Denominazione = "fgf";
            anagraficaType1.IdCodice = "l";
        }

        private ICommand _addIndirizzoResaCommand;

        public ICommand AddIndirizzoResaCommand
        {
            get
            {
                if ( _addIndirizzoResaCommand != null ) return _addIndirizzoResaCommand;
                _addIndirizzoResaCommand = new RelayCommand( param => AddDatiIndirizzoResa(),
                    ( e ) => Instance?.DatiGenerali.DatiTrasporto != null &&
                             Instance.DatiGenerali.DatiTrasporto.IndirizzoResa == null );
                return _addIndirizzoResaCommand;
            }
        }

        private void AddDatiIndirizzoResa()
        {
            var current = CurrentPoco as DatiGeneraliType;
            if ( current == null ) return;

            object instance = new IndirizzoType();
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            ( ( IValidatable ) instance ).Validate();

            ObjectExplorer.TryProxiedAllInstances<BaseEntity>( ref instance, "FaPA.Core" );

            current.DatiTrasporto.IndirizzoResa = ( IndirizzoType ) instance;

            HookChanged( instance );
        }

        #endregion

        //ctor
        public DatiGeneraliViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, ( Core.Fattura f ) => f.DatiGenerali, "Dati generali", false )
        {

            DatiOrdini = new DatiOrdineTabViewModel( this, instance );
            DatiOrdini.Init();

            DatiContratto = new DatiContrattoTabViewModel( this, instance );
            DatiContratto.Init();

            DatiConvenzione = new DatiConvenzioneTabViewModel( this, instance );
            DatiConvenzione.Init();

            DatiFattureCollegate = new DatiFattureCollegateTabViewModel( this, instance );
            DatiFattureCollegate.Init();

            DatiRicezione = new DatiRicezioneTabViewModel( this, instance );
            DatiRicezione.Init();

            CurrentEntityChanged += OnCurrentFatturaChanged;
        }

        private void OnCurrentFatturaChanged( object sender, PropertyChangedEventArgs eventarg )
        {
            if ( sender is DatiTrasportoType )
            {
                var anagraficaType = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto.DatiAnagraficiVettore.Anagrafica;
                var bggg = anagraficaType is IProxy;
                var jjj = sender is IProxy;
                anagraficaType.Denominazione = "";
            }

            if ( sender is IndirizzoType )
            {
                var datiTraporto = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto;
                datiTraporto.Validate();
                ( ( IValidatable ) datiTraporto ).HandleValidationResults( "IndirizzoResa", datiTraporto );
            }

            if ( sender is IdFiscaleType || sender is AnagraficaType )
            {
                var datiTraporto = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto.DatiAnagraficiVettore;
                datiTraporto.Validate();
                ( ( IValidatable ) datiTraporto ).HandleValidationResults( "DatiAnagraficiVettore", datiTraporto );
            }
        }


        protected override void HookOnChanged( object poco )
        {
            var entity = poco as DatiGeneraliType;
            if ( entity == null ) return;

            HookChanged( entity );

            if ( entity.FatturaPrincipale != null )
            {
                HookChanged( entity.FatturaPrincipale );
            }

            if ( entity.DatiDDT != null )
            {
                HookChanged( entity.DatiDDT );
            }

            if ( entity.DatiSAL != null )
            {
                HookChanged( entity.DatiSAL );
            }

            if ( entity.DatiTrasporto != null )
            {
                HookChanged( entity.DatiTrasporto );
            }

            if ( entity.DatiFattureCollegate != null )
            {
                foreach ( var dettaglio in entity.DatiFattureCollegate )
                {
                    HookChanged( dettaglio );
                }
            }

            if ( entity.DatiContratto != null )
            {
                foreach ( var dettaglio in entity.DatiContratto )
                {
                    HookChanged( dettaglio );
                }
            }

            if ( entity.DatiConvenzione != null )
            {
                foreach ( var dettaglio in entity.DatiConvenzione )
                {
                    HookChanged( dettaglio );
                }
            }

            if ( entity.DatiOrdineAcquisto != null )
            {
                foreach ( var dettaglio in entity.DatiOrdineAcquisto )
                {
                    HookChanged( dettaglio );
                }
            }

            if ( entity.DatiRicezione != null )
            {
                foreach ( var dettaglio in entity.DatiRicezione )
                {
                    HookChanged( dettaglio );
                }
            }
        }

        protected override void OnRequestClose()
        {
            if ( UserProperty != null )
            {
                const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
                MessageBox.Show( lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation );
                return;
            }

            base.OnRequestClose();
        }

    }
}