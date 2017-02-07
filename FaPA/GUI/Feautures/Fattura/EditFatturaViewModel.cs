using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using NHibernate;
using System.Linq;
using System.Threading.Tasks;
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class EditFatturaViewModel : EditViewModel<Core.Fattura>
    {
        private DatiGeneraliViewModel _datiGeneraliViewModel;
        public DatiGeneraliViewModel DatiGeneraliViewModel
        {
            get { return _datiGeneraliViewModel; }
            set
            {
                if ( Equals( value, _datiGeneraliViewModel ) ) return;
                _datiGeneraliViewModel = value;
                NotifyOfPropertyChange( () => DatiGeneraliViewModel );
            }
        }

        private DatiDocumentoViewModel _datiDocumentoViewModel;
        public DatiDocumentoViewModel DatiDocumentoViewModel
        {
            get { return _datiDocumentoViewModel; }
            set
            {
                if (Equals(value, _datiDocumentoViewModel)) return;
                _datiDocumentoViewModel = value;
                NotifyOfPropertyChange(() => DatiDocumentoViewModel);
            }
        }

        private DettagliFatturaViewModel _dettagliFatturaViewModel;
        public DettagliFatturaViewModel DettagliFatturaViewModel
        {
            get { return _dettagliFatturaViewModel; }
            set
            {
                _dettagliFatturaViewModel = value;
                NotifyOfPropertyChange( () => DettagliFatturaViewModel );
            }
        }
        
        public TrasmittenteTabViewModel TrasmittenteViewModel { get; set; }

        public DatiPagamentoTabViewModel DatiPagamentoViewModel { get; set; }

        public AllegatiViewModel AllegatiViewModel
        {
            get { return _allegatiViewModel; }
            set
            {
                if (Equals(value, _allegatiViewModel)) return;
                _allegatiViewModel = value;
                NotifyOfPropertyChange(() => AllegatiViewModel);
            }
        }

        //public DatiRitenutaViewModel DatiRitenutaViewModel { get; set; }

        //ctor
        public EditFatturaViewModel(IBasePresenter baseCrudPresenter, IList userEntities, 
            ICollectionView userCollectionView, ISession session): base(baseCrudPresenter, userEntities, userCollectionView)
        {           
            SetUpSession(session, null);
            CurrentEntityChanged += OnCurrentFatturaChanged;
        }

        #region Overrides

        public override string EditTemplateName => "FatturaTemplate";

        protected override Core.Fattura CreateInstance()
        {
            object entity = Activator.CreateInstance( typeof( Core.Fattura ) );

            ( ( Core.Fattura ) entity ).Init();

            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntity>( ref entity, "FaPA.Core" );

            return ( Core.Fattura ) entity ;
        }

        public override void CreateNewEntity()
        {
            CurrentEntity = CreateInstance();
          
            InitFatturaTabs();

        }


        protected override void DefaultCancelOnEditAction()
        {
            base.DefaultCancelOnEditAction();
            InitFatturaTabs();
            DettagliFatturaViewModel.UserCollectionView.Refresh();
        }

        protected override bool TrySaveCurrentEntity()
        {
           CurrentEntity.SetTrasmittente();

           return base.TrySaveCurrentEntity();
        }

        protected override void Dispose()
        {
            CurrentEntityChanged -= OnCurrentFatturaChanged;
            base.Dispose();
        }

        #endregion

        public override void OnPageGotFocus()
        {
            base.OnPageGotFocus();

            if ( DettagliFatturaViewModel == null )
                InitFatturaTabs();
            else
                InitFatturaTabs();
        }

        private void OnCurrentFatturaChanged(Core.Fattura currententity)
        {
            InitFatturaTabs();

            if (currententity == null || currententity.Id == 0)
            {
                DettagliFatturaVisibility = Visibility.Collapsed;
                EmptyMsgVisibility = Visibility.Visible;
            }
            else
            {
                DettagliFatturaVisibility = Visibility.Visible;
                EmptyMsgVisibility = Visibility.Collapsed;          
            }
        }

        private void InitFatturaTabs()
        {
            var fattura = CurrentEntity;

            DettagliFatturaViewModel = new DettagliFatturaViewModel(this, fattura.DatiBeniServizi );
            DettagliFatturaViewModel.Init();
            DettagliFatturaViewModel.CurrentEntityChanged += OnDettaglioFatturaPropertyChanged;

            DatiGeneraliViewModel = new DatiGeneraliViewModel( this, fattura.FatturaElettronicaBody );
            AddTabViewModel<DatiGeneraliViewModel>( DatiGeneraliViewModel );

            DatiDocumentoViewModel = new DatiDocumentoViewModel(this, fattura.DatiGenerali );
            AddTabViewModel<DatiDocumentoViewModel>(DatiDocumentoViewModel);

            TrasmittenteViewModel = new TrasmittenteTabViewModel(this, fattura);
            TrasmittenteViewModel.Init();
            AddTabViewModel<TrasmittenteTabViewModel>( TrasmittenteViewModel );

            DatiPagamentoViewModel = new DatiPagamentoTabViewModel( this, fattura );
            DatiPagamentoViewModel.Init();
            AddTabViewModel<DatiPagamentoTabViewModel>( DatiPagamentoViewModel );

            AllegatiViewModel = new AllegatiViewModel( this, fattura.FatturaElettronicaBody);
            AllegatiViewModel.Init();
            AddTabViewModel<AllegatiViewModel>( AllegatiViewModel );

            //if (fattura?.Ritenuta != null)
            //{
            //    AddTabRitenuta();
            //}

            //if ( fattura?.DatiOrdineAcquisto != null )
            //{
            //    AddTabOrdine();
            //}

            //if (fattura?.DatiContratto != null)
            //{
            //    AddTabContratto();
            //}

            //if (fattura?.DatiConvenzione != null)
            //{
            //    AddTabConvenzione();
            //}

        }

        //private void OnDatiGeneraliPropertyChanged( object sender, PropertyChangedEventArgs eventarg )
        //{
        //    OnChildChanged();
        //}


        private void OnDettaglioFatturaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChildChanged();
        }

        private void OnChildChanged()
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            IsInEditing = true;
            AllowSave = IsValidate();
        }

        private bool IsValidate()
        {
            //Validate();
            var dettagliFattura = DettagliFatturaViewModel == null || DettagliFatturaViewModel.IsValid;
            return IsValid && dettagliFattura;
        }

        #region entities events publisher

        public override void PublishAddedNewEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new FatturaAddedNew {Dto = (FatturaDto) CurrentDtoEntity}, this);
        }

        public override void PublishUpdateEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new FatturaUpdated {Dto = (FatturaDto) CurrentDtoEntity}, this);
        }

        public override void PublishDeletedEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new FatturaDeleted {Dto = (FatturaDto) dto}, this);
        }

        #endregion

        #region UI commands


        private void AddTabViewModel<TV>(WorkspaceViewModel tabViewModel)
        {
            // Dispatcher.CurrentDispatcher.BeginInvoke(new System.Action(() =>
            //{
            //    BasePresenter.Workspaces.Add( _ritenutaViewModel = ritenutaViewModel );          
            //} ));
            var vm = BasePresenter.Workspaces.FirstOrDefault(a => a is TV);
            if (vm == null)
                BasePresenter.Workspaces.Add(tabViewModel);
        }

        private ICommand _generateXmlStreamCommand;
        public ICommand GenerateXmlStreamCommand
        {
            get
            {
                if (_generateXmlStreamCommand != null)
                    return _generateXmlStreamCommand;
                _generateXmlStreamCommand = new RelayCommand(c => ValidateAndFlushXml(CurrentEntity));
                return _generateXmlStreamCommand;
            }
        }

        private void ValidateAndFlushXml(IValidatable fattura)
        {
            //if ( fattura.FatturaElettronicaHeader.DatiTrasmissione == null )
            //{
            //fattura.SetTrasmittente();
            //}

            string path = null;
            string error = null;
            FlushXml( fattura, out path, out error );
        }

        private ICommand _showXmlIntoTreeViewCommand;
        public ICommand ShowXmlIntoTreeViewCommand
        {
            get
            {
                if (_showXmlIntoTreeViewCommand != null) return _showXmlIntoTreeViewCommand;
                _showXmlIntoTreeViewCommand = new RelayCommand(param => OnOpendXml(), param => true);
                return _showXmlIntoTreeViewCommand;
            }

        }

        private ICommand _showXmlIntoBrowserCommand;
        public ICommand ShowXmlIntoBrowserCommand
        {
            get
            {
                if (_showXmlIntoBrowserCommand != null) return _showXmlIntoBrowserCommand;
                _showXmlIntoBrowserCommand = new RelayCommand(param => OnOpenXmlToBrowser(), param => true);
                return _showXmlIntoBrowserCommand;
            }

        }

        private void OnOpenXmlToBrowser()
        {
            string path = null;
            string error = null;
            FlushXml(CurrentEntity, out path, out error);
            var browser = "FireFox.exe";
            try
            {
                Process.Start(browser, path);
            }
            catch (Exception)
            {
                MessageBox.Show("Apertura file nel browser internet interrota", 
                                "Errore durante l'apertura del file...", MessageBoxButton.OK);
            }
        }

        private void OnOpendXml()
        {
            if (CurrentEntity == null) return;
            Presenters.Show("ShowXmlToTreeView", CurrentEntity.GetXmlDocument());
        }

        private bool FlushXml(IValidatable fattura, out string path, out string error)
        {
            ShowCursor.Show();
            if ( ValidateAndCreateXmlStream(fattura, out path, out error))
            {
                var msg = "Fattura validata." + Environment.NewLine + "Il file " + path + " è stato salvato sul desktop";
                MessageBox.Show(msg, "Salvataggio completato", MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show( error, "Fattura non generata...", MessageBoxButton.OK);
                return false;
            }
        }

        private bool ValidateAndCreateXmlStream(IValidatable fattura, out string outPath, out string error)
        {
            error = null;

            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var nomeFile = CurrentEntity.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdPaese +
                           CurrentEntity.AnagraficaCedenteDB.CodiceFiscale + "_" +
                           CurrentEntity.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.ProgressivoInvio + ".xml";
            outPath = Path.Combine(folderPath, nomeFile);

            //ShowCursor.Show();
            var errors = fattura.Validate();
            if (!errors.Success)
            {
                error = "Fattura non validata: " + Environment.NewLine + string.Join("; ", errors);
                return false;
            }
            var xmlDoc = CurrentEntity.GetXmlDocument();
            xmlDoc.Save(outPath);
            return true;
        }

        private Visibility _emptyMesgVisibility = Visibility.Collapsed;
        public Visibility EmptyMsgVisibility
        {
            get
            {
                return _emptyMesgVisibility;
            }
            set
            {
                _emptyMesgVisibility = value;
                NotifyOfPropertyChange(() => EmptyMsgVisibility);
            }
        }

        private Visibility _dettagliFatturaVisibility;
        private AllegatiViewModel _allegatiViewModel;

        public Visibility DettagliFatturaVisibility
        {
            get { return _dettagliFatturaVisibility; }
            set
            {
                _dettagliFatturaVisibility = value;
                NotifyOfPropertyChange(() => DettagliFatturaVisibility);
            }
        }

        #endregion
    }
}
