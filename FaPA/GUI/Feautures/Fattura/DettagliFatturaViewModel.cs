using System;
using System.ComponentModel;
using System.Windows.Data;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DettagliFatturaViewModel : BaseTabsViewModel<Core.Fattura, DettaglioLineeType[]>
    {
        private bool _isOnInit;

        private ScontoMaggiorazioneViewModel _scontoMaggiorazioneViewModel;
        public ScontoMaggiorazioneViewModel ScontoMaggiorazioneViewModel
        {
            get { return _scontoMaggiorazioneViewModel; }
            set
            {
                if ( Equals( value, _scontoMaggiorazioneViewModel ) ) return;
                _scontoMaggiorazioneViewModel = value;
                NotifyOfPropertyChange( () => ScontoMaggiorazioneViewModel );
            }
        }

        private AltriDatiViewModel _altridatiViewModel;
        public AltriDatiViewModel AltridatiViewModel
        {
            get { return _altridatiViewModel; }
            set
            {
                _altridatiViewModel = value;
                NotifyOfPropertyChange(() => AltridatiViewModel);
            }
        }

        public DettagliFatturaViewModel( IRepository repository, Core.Fattura instance ) :
            base( f => f.DettaglioLinee, repository, instance, "", false )
        {
        }

        public override void Init()
        {
            _isOnInit = true;

            base.Init();

            var dettaglio = UserCollectionView?.CurrentItem as DettaglioLineeType;
            if ( dettaglio == null ) return;

            InitAltriChildViewModel( dettaglio );
            _isOnInit = false;
        }

        protected override void PersitEntity()
        {
            Instance.SyncFatturaPa();
            base.PersitEntity();
        }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
            if ( UserProperty == null ) return;
            var lastAdded= UserProperty[UserProperty.Length - 1];
            InitAltriChildViewModel( lastAdded );
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }

        protected override void OnCurrentChanged( object sender, EventArgs e )
        {
            if ( _isOnInit ) return;

            var cview = sender as ListCollectionView;
            var dettaglio = cview?.CurrentItem as DettaglioLineeType;

            if ( dettaglio == null ) return;

            InitAltriChildViewModel( dettaglio );

            base.OnCurrentChanged( sender, e );
        }

        private void InitAltriChildViewModel( DettaglioLineeType dettaglio )
        {
            AltridatiViewModel = new AltriDatiViewModel( this, dettaglio );
            AltridatiViewModel.Init();
            AltridatiViewModel.CurrentEntityChanged += OnAltriDatiPropertyChanged;

            ScontoMaggiorazioneViewModel = new ScontoMaggiorazioneViewModel( this, dettaglio );
            ScontoMaggiorazioneViewModel.Init();
            ScontoMaggiorazioneViewModel.CurrentEntityChanged += OnScontoMaggiorazionePropertyChanged;

            AllowSave = IsValidate();
        }

        private void OnAltriDatiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            
            var dettaglio = UserCollectionView.CurrentItem as DettaglioLineeType;
            if (dettaglio == null) return;

            ( ( IValidatable ) dettaglio).ValidatePropertyValue( nameof( dettaglio.AltriDatiGestionali ) );

            AllowSave = IsValidate();

            base.OnPropChanged(dettaglio, e);

        }

        private void OnScontoMaggiorazionePropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;

            var dettaglio = UserCollectionView.CurrentItem as DettaglioLineeType;
            if ( dettaglio == null ) return;

            ( ( IValidatable ) dettaglio ).ValidatePropertyValue( nameof(dettaglio.ScontoMaggiorazione) );

            AllowSave = IsValidate();

            base.OnPropChanged( dettaglio, e );

        }

        public override bool Delete()
        {
            SetUserProperty( null);
            return base.Persist(Instance);
        }

        private bool IsValidate()
        {
            var isValidAltriDatiViewModel = AltridatiViewModel == null || AltridatiViewModel.IsValid;
            var isValidScontoMaggiorazioneViewModel = ScontoMaggiorazioneViewModel == null || ScontoMaggiorazioneViewModel.IsValid;
            return isValidAltriDatiViewModel && isValidScontoMaggiorazioneViewModel ;
        }


    }

}