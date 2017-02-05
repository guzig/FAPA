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

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliViewModel : WorkspaceViewModel
    {
        #region fields
        private DatiSalTabViewModel _datiSalViewModel;
        private DatiDdtTabViewModel _datiDdtViewModel;
        private DatiOrdineTabViewModel _datiOrdini;
        private DatiContrattoTabViewModel _datiContratto;
        private DatiConvenzioneTabViewModel _datiConvenzione;
        private DatiFattureCollegateTabViewModel _datiFattureCollegate;
        private DatiRicezioneTabViewModel _datiRicezione;

        #endregion

        #region props

        public DatiDdtTabViewModel DatiDdtViewModel
        {
            get { return _datiDdtViewModel; }
            set
            {
                if (Equals(value, _datiDdtViewModel)) return;
                _datiDdtViewModel = value;
                NotifyOfPropertyChange(() => DatiDdtViewModel);
            }
        }

        public DatiFatturaPrincipaleViewModel DatiFatturaPrincipaleViewModel
        {
            get { return _datiFatturaPrincipaleViewModel; }
            set
            {
                if ( Equals( value, _datiFatturaPrincipaleViewModel ) ) return;
                _datiFatturaPrincipaleViewModel = value;
                NotifyOfPropertyChange( () => DatiFatturaPrincipaleViewModel );
            }
        }

        public DatiSalTabViewModel DatiSalViewModel
        {
            get { return _datiSalViewModel; }
            set
            {
                if (Equals(value, _datiSalViewModel)) return;
                _datiSalViewModel = value;
                NotifyOfPropertyChange(() => DatiSalViewModel);
            }
        }

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

        private ICommand _addSchedaCommand;
        private DatiFatturaPrincipaleViewModel _datiFatturaPrincipaleViewModel;

        //public ICommand AddSchedaCommand
        //{
        //    get
        //    {
        //        if (_addSchedaCommand != null) return _addSchedaCommand;
        //        _addSchedaCommand = new RelayCommand( AddDatiScheda, param => true );
        //        return _addSchedaCommand;
        //    }
        //}

        //private void AddDatiVettore()
        //{
        //    var current = CurrentPoco as DatiGeneraliType;
        //    if ( current == null ) return;

        //    object instance = new DatiAnagraficiVettoreType();
        //    ( ( DatiAnagraficiVettoreType ) instance ).Anagrafica = new AnagraficaType();
        //    ( ( DatiAnagraficiVettoreType ) instance ).IdFiscaleIVA = new IdFiscaleType();
        //    LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

        //    ( ( IValidatable ) instance ).Validate();

        //    ObjectExplorer.TryProxiedAllInstances<BaseEntity>( ref instance, "FaPA.Core" );

        //    current.DatiTrasporto.DatiAnagraficiVettore = ( DatiAnagraficiVettoreType ) instance;

        //    HookChanged( instance );

        //    HookChanged( ( ( DatiAnagraficiVettoreType ) instance ).IdFiscaleIVA );
        //    HookChanged( ( ( DatiAnagraficiVettoreType ) instance ).Anagrafica );
        //}

        //private void AddDatiIndirizzoResa()
        //{
        //    var current = CurrentPoco as DatiGeneraliType;
        //    if ( current == null ) return;

        //    object instance = new IndirizzoType();
        //    LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

        //    ( ( IValidatable ) instance ).Validate();

        //    ObjectExplorer.TryProxiedAllInstances<BaseEntity>( ref instance, "FaPA.Core" );

        //    current.DatiTrasporto.IndirizzoResa = ( IndirizzoType ) instance;

        //    HookChanged( instance );
        //}

        #endregion


        //private void AddDatiScheda(object parm)
        //{
        //    var current = CurrentPoco as DatiGeneraliType;
        //    if (current == null) return;
        //    string param = (string) parm;
        //    switch (param)
        //    {
        //        case "FatturaPrincipale":
        //            GetInstance<FatturaPrincipaleType>(v => current.FatturaPrincipale = v);
        //            break;

        //        case "DatiTrasporto":
        //            GetInstance<DatiTrasportoType>(v => current.DatiTrasporto = v);
        //            break;

        //        case "DatiVettore":
        //            AddDatiVettore();
        //            break;

        //        case "IndirizzoResa":
        //            AddDatiIndirizzoResa();
        //            break;
        //    }
        //}
        
        //private void GetInstance<T>(Action<T> prop)
        //{
        //    object instance = Activator.CreateInstance<T>();
        //    LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

        //    ((IValidatable)instance).Validate();

        //    ObjectExplorer.TryProxiedAllInstances<BaseEntity>(ref instance, "FaPA.Core");

        //    HookChanged(instance);

        //    prop((T) instance);
        //}

        //ctor
        public DatiGeneraliViewModel( IRepository repository, FatturaElettronicaBodyType instance ) 
        {
            DisplayName = "Dati generali";
            IsCloseable = false;
            InitChildViewModels(instance, repository);

            //CurrentEntityChanged += OnCurrentFatturaChanged;
        }

        //protected override void OnCancelDelegateExecute()
        //{
        //    base.OnCancelDelegateExecute();
        //    InitChildViewModels(Instance);
        //}

        private void InitChildViewModels( FatturaElettronicaBodyType instance, IRepository repository )
        {
            DatiFatturaPrincipaleViewModel = new DatiFatturaPrincipaleViewModel( repository, instance.DatiGenerali );
            DatiFatturaPrincipaleViewModel.Init();

            DatiDdtViewModel = new DatiDdtTabViewModel( repository, instance.DatiGenerali);
            DatiDdtViewModel.Init();

            DatiSalViewModel = new DatiSalTabViewModel( repository, instance.DatiGenerali);
            DatiSalViewModel.Init();

            DatiOrdini = new DatiOrdineTabViewModel( repository, instance.DatiGenerali );
            DatiOrdini.Init();

            DatiContratto = new DatiContrattoTabViewModel( repository, instance.DatiGenerali );
            DatiContratto.Init();

            DatiConvenzione = new DatiConvenzioneTabViewModel( repository, instance.DatiGenerali );
            DatiConvenzione.Init();

            DatiFattureCollegate = new DatiFattureCollegateTabViewModel( repository, instance.DatiGenerali );
            DatiFattureCollegate.Init();

            DatiRicezione = new DatiRicezioneTabViewModel( repository, instance.DatiGenerali );
            DatiRicezione.Init();
        }

        //private void OnCurrentFatturaChanged( object sender, PropertyChangedEventArgs eventarg )
        //{

        //    if ( sender is IndirizzoType )
        //    {
        //        var datiTraporto = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto;
        //        datiTraporto.Validate();
        //        ( ( IValidatable ) datiTraporto ).HandleValidationResults( "IndirizzoResa" );
        //    }

        //    if ( sender is IdFiscaleType || sender is AnagraficaType )
        //    {
        //        var datiTraporto = ( ( DatiGeneraliType ) CurrentPoco ).DatiTrasporto;
        //        datiTraporto.Validate();
        //        ( ( IValidatable ) datiTraporto ).HandleValidationResults( "DatiAnagraficiVettore" );

        //        ((IValidatable)datiTraporto.DatiAnagraficiVettore).
        //            HandleValidationResults("IdFiscaleIVA");

        //        ((IValidatable)datiTraporto.DatiAnagraficiVettore).
        //            HandleValidationResults("Anagrafica");

        //    }
        //}


        //protected override void HookOnChanged( object poco )
        //{
        //    var entity = poco as DatiGeneraliType;
        //    if ( entity == null ) return;

        //    HookChanged( entity );

        //    if ( entity.FatturaPrincipale != null )
        //    {
        //        HookChanged( entity.FatturaPrincipale );
        //    }

        //    if ( entity.DatiDDT != null )
        //    {
        //        HookChanged( entity.DatiDDT );
        //    }

        //    if ( entity.DatiSAL != null )
        //    {
        //        HookChanged( entity.DatiSAL );
        //    }

        //    if ( entity.DatiTrasporto != null )
        //    {
        //        HookChanged( entity.DatiTrasporto );
        //    }

        //    if ( entity.DatiFattureCollegate != null )
        //    {
        //        foreach ( var dettaglio in entity.DatiFattureCollegate )
        //        {
        //            HookChanged( dettaglio );
        //        }
        //    }

        //    if ( entity.DatiContratto != null )
        //    {
        //        foreach ( var dettaglio in entity.DatiContratto )
        //        {
        //            HookChanged( dettaglio );
        //        }
        //    }

        //    if ( entity.DatiConvenzione != null )
        //    {
        //        foreach ( var dettaglio in entity.DatiConvenzione )
        //        {
        //            HookChanged( dettaglio );
        //        }
        //    }

        //    if ( entity.DatiOrdineAcquisto != null )
        //    {
        //        foreach ( var dettaglio in entity.DatiOrdineAcquisto )
        //        {
        //            HookChanged( dettaglio );
        //        }
        //    }

        //    if ( entity.DatiRicezione != null )
        //    {
        //        foreach ( var dettaglio in entity.DatiRicezione )
        //        {
        //            HookChanged( dettaglio );
        //        }
        //    }
        //}

        //protected override void OnRequestClose()
        //{
        //    if ( UserProperty != null )
        //    {
        //        const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
        //        MessageBox.Show( lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation );
        //        return;
        //    }

        //    base.OnRequestClose();
        //}


        //public override object Read()
        //{
        //    var root = Repository.Read();
        //    Instance = ( ( Core.Fattura ) root).FatturaElettronicaBody;
        //    var userProp = GetterProp( Instance );
        //    return userProp;
        //}
    }
}