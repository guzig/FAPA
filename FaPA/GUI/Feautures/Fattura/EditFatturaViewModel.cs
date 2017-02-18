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
using FaPA.AppServices.CoreValidation;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class EditFatturaViewModel : EditViewModel<Core.Fattura>
    {
        private const string XsdValidationErrorIcon = @"/GUI/Design\Styles\Images\1366144159_error.png";
        private const string XsdValidationPassedIcon = @"/GUI/Design\Styles\Images\Button-Ok-icon.png";

        public virtual string DomainResultFatturaPA
        {
            get { return _domainResultFatturaPa; }
            set
            {
                if ( value == _domainResultFatturaPa ) return;
                _domainResultFatturaPa = value;                   
                NotifyOfPropertyChange( () => DomainResultFatturaPA );
            }
        }

        public string IconXsdValidationState
        {
            get { return _iconXsdValidationState; }
            set
            {
                if ( value == _iconXsdValidationState ) return;
                _iconXsdValidationState = value;
                NotifyOfPropertyChange( () => IconXsdValidationState );
            }
        }

        public bool IsCopyMode { get; set; } = false;

        #region ViewModels

        private DatiGeneraliViewModel _datiGeneraliViewModel;

        public DatiGeneraliViewModel DatiGeneraliViewModel
        {
            get { return _datiGeneraliViewModel; }
            set
            {
                if (Equals(value, _datiGeneraliViewModel)) return;
                _datiGeneraliViewModel = value;
                NotifyOfPropertyChange(() => DatiGeneraliViewModel);
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
                NotifyOfPropertyChange(() => DettagliFatturaViewModel);
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

        public DatiRappresentanteFiscaleViewModel RappresentanteFiscaleViewModel
        {
            get { return _rappresentanteFiscaleViewModel; }
            set
            {
                if (Equals(value, _rappresentanteFiscaleViewModel)) return;
                _rappresentanteFiscaleViewModel = value;
                NotifyOfPropertyChange(() => RappresentanteFiscaleViewModel);
            }
        }

        public DatiTerzoIntermediarioViewModel TerzoIntermediarioViewModel
        {
            get { return _terzoIntermediarioViewModel; }
            set
            {
                if (Equals(value, _terzoIntermediarioViewModel)) return;
                _terzoIntermediarioViewModel = value;
                NotifyOfPropertyChange(() => TerzoIntermediarioViewModel);
            }
        }

        public DatiRiepilogoIvaViewModel DatiRiepilogoIvaViewModel
        {
            get { return _datiRiepilogoIvaViewModel; }
            set
            {
                if (Equals(value, _datiRiepilogoIvaViewModel)) return;
                _datiRiepilogoIvaViewModel = value;
                NotifyOfPropertyChange(() => DatiRiepilogoIvaViewModel);
            }
        }

        #endregion


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
            var entity = Activator.CreateInstance<Core.Fattura>();
            entity.DataFatturaDB = DateTime.Now.Date;
            entity.Init();
            return ( Core.Fattura ) ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntity>( entity );
        }

        public override void CreateNewEntity()
        {
            if ( IsCopyMode )
            {
                var copy = CurrentEntity.Copy();
                copy.DataFatturaDB = DateTime.Now.Date;
                copy.NumeroFatturaDB = CurrentEntity.NumeroFatturaDB + " ? ";
                var ana1 = copy.AnagraficaCedenteDB;
                var ana2 = copy.AnagraficaCommittenteDB;
                copy.AnagraficaCedenteDB = null;
                copy.AnagraficaCommittenteDB = null;
                CurrentEntity = ( Core.Fattura ) ObjectExplorer.DeepProxiedCopyOfType<FaPA.Core.BaseEntity>( copy );
                CurrentEntity.AnagraficaCedenteDB = ana1;
                CurrentEntity.AnagraficaCommittenteDB = ana2;
            }
            else
            {
                CurrentEntity = CreateInstance();
            }

            InitFatturaTabs( CurrentEntity );
        }
        
        protected override void DefaultCancelOnEditAction()
        {
            base.DefaultCancelOnEditAction();
            InitFatturaTabs( CurrentEntity );
            DettagliFatturaViewModel.UserCollectionView?.Refresh();
        }

        protected override bool TrySaveCurrentEntity()
        {
            CurrentEntity.SyncFatturaPa();
            var result = base.TrySaveCurrentEntity();
            return result;
        }

        protected override void Dispose()
        {
            CurrentEntityChanged -= OnCurrentFatturaChanged;
            base.Dispose();
        }

        #endregion

        //public override void OnPageGotFocus()
        //{
        //    base.OnPageGotFocus();

        //    InitFatturaTabs();
        //}

        private void OnCurrentFatturaChanged(Core.Fattura currententity)
        {
            InitFatturaTabs( CurrentEntity );
        }

        private void InitFatturaTabs(Core.Fattura fattura )
        {
            //var fattura = CurrentEntity;

            DettagliFatturaViewModel = new DettagliFatturaViewModel(this, fattura );
            DettagliFatturaViewModel.Init();
            DettagliFatturaViewModel.CurrentEntityPropChanged += OnDettaglioFatturaPropertyPropChanged;
            DettagliFatturaViewModel.CurrentEntityChanged += OnDettaglioFatturaCurrentChanged;

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

            RappresentanteFiscaleViewModel = new DatiRappresentanteFiscaleViewModel( this, fattura );
            RappresentanteFiscaleViewModel.Init();
            AddTabViewModel<DatiRappresentanteFiscaleViewModel>( RappresentanteFiscaleViewModel );
            
            TerzoIntermediarioViewModel = new DatiTerzoIntermediarioViewModel( this, fattura );
            TerzoIntermediarioViewModel.Init();
            AddTabViewModel<DatiTerzoIntermediarioViewModel>( TerzoIntermediarioViewModel );

            DatiRiepilogoIvaViewModel = new DatiRiepilogoIvaViewModel(this, fattura.DatiBeniServizi);
            DatiRiepilogoIvaViewModel.Init();
            AddTabViewModel<DatiRiepilogoIvaViewModel>(DatiRiepilogoIvaViewModel);

            var isValidatedByXsd = fattura.ValidateByXsdFatturaPA( );

            if ( string.IsNullOrWhiteSpace( isValidatedByXsd ) )
            {
                IconXsdValidationState = XsdValidationPassedIcon;
                DomainResultFatturaPA = "Validazione riuscita";
            }
            else
            {
                IconXsdValidationState = XsdValidationErrorIcon;
                DomainResultFatturaPA = isValidatedByXsd;
            }

        }

        private void OnDettaglioFatturaCurrentChanged( object currententity )
        {
            OnChildChanged();
        }

        private void OnDettaglioFatturaPropertyPropChanged(object sender, PropertyChangedEventArgs e)
        {

            OnChildChanged();
        }

        private void OnChildChanged()
        {
            Validate();
            if ( DettagliFatturaViewModel.IsEditing )
            {
                LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
                IsInEditing = true;
                AllowSave = IsValidate();
            }
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
            //EventPublisher.Publish(new FatturaUpdated {e = (Core.Fattura) CurrentDtoEntity}, this);
        }

        public override void PublishDeletedEntityEvent(BaseEntity dto)
        {
            //EventPublisher.Publish(new FatturaDeleted {Dto = (FatturaDto) dto}, this);
        }

        #endregion

        #region UI commands


        private ICommand _showAnagraficaCommand;
        public ICommand ShowAnagraficaCommand
        {
            get
            {
                if ( _showAnagraficaCommand != null )
                    return _showAnagraficaCommand;
                _showAnagraficaCommand = new RelayCommand( ShowAnagrafica, r=>true );
                return _showAnagraficaCommand;
            }
        }

        private void ShowAnagrafica(object c )
        {
            string param = ( string ) c;

            long id;
            if ( param != null && param == "Fornitore" )
                id = CurrentEntity.AnagraficaCedenteDB.Id;
            else if ( param != null && param == "Committente" )
                id = CurrentEntity.AnagraficaCommittenteDB.Id;
            else return;

            Presenters.Show("Anagrafica", new Action<GUI.Feautures.Anagrafica.Presenter>( p => p.CreateNewModel( 1, id )  ) );
        }

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
                _generateXmlStreamCommand = new RelayCommand(c => ValidateAndFlushXml(CurrentEntity), x => !IsInEditing);
                return _generateXmlStreamCommand;
            }
        }

        private void ValidateAndFlushXml(Core.Fattura fattura)
        {
            string error = null;
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var path = GetOutPath( folderPath );
            FlushXml( fattura, path, out error );
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
            string error = null;
            const string path = "tempfatt.xml";

            var isValid = ValidateAndCreateXmlStream( CurrentEntity, path, out error);

            const string browser = "FireFox.exe";
            try
            {
                Process.Start(browser, path);
            }
            catch (Exception)
            {
                MessageBox.Show("Apertura file nel browser internet interrota",
                                "Errore durante l'apertura del file...", MessageBoxButton.OK);
            }

            if ( !isValid )
            {
                const string caption = "Fattura non validata.";
                MessageBox.Show(caption + Environment.NewLine + error, caption, MessageBoxButton.OK);
            }
        }

        private void OnOpendXml()
        {
            if (CurrentEntity == null) return;
            Presenters.Show("ShowXmlToTreeView", CurrentEntity.GetXmlFatturaPA());
        }


        private ICommand _addCopy;
        public ICommand AddCopy
        {
            get
            {
                if (_addCopy != null) return _addCopy;
                _addCopy = new RelayCommand(param => AddNewCopy(), param => !IsInEditing);
                return _addCopy;
            }

        }

        private void AddNewCopy()
        {
            IsCopyMode = true;
            AddNew();
            IsCopyMode = false;
        }

        private bool FlushXml(Core.Fattura fattura, string path, out string error)
        {
            ShowCursor.Show();
            if ( ValidateAndCreateXmlStream(fattura, path, out error))
            {
                var msg = "Fattura validata esportata." + Environment.NewLine + "Il file " + path + " � stato salvato sul desktop";
                MessageBox.Show(msg, "Salvataggio completato", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                var msg = "Fattura NON validata esportata." + Environment.NewLine + "Il file " + path + " � stato salvato sul desktop";
                MessageBox.Show( msg, "Fattura non validata...", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool ValidateAndCreateXmlStream(Core.Fattura fattura, string outPath, out string error)
        {
            error = null;

            ShowCursor.Show();

            var internalErrors = fattura.Validate();
            var DomainResultFatturaPA = fattura.ValidateByXsdFatturaPA();

            if (!internalErrors.Success || !string.IsNullOrWhiteSpace( DomainResultFatturaPA ) )
            {
                error = "Fattura non validata: " + Environment.NewLine + string.Join("; ", DomainResultFatturaPA );
            }
            var xmlDoc = CurrentEntity.GetXmlFatturaPA();
            xmlDoc.Save(outPath);
            return true;
        }

        private string GetOutPath( string folderPath)
        {
            var nomeFile = CurrentEntity.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdPaese +
                           CurrentEntity.AnagraficaCedenteDB.CodiceFiscale + "_" +
                           CurrentEntity.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.ProgressivoInvio + ".xml";
            var outPath = Path.Combine(folderPath, nomeFile);
            return outPath;
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

        private AllegatiViewModel _allegatiViewModel;

        private DatiRappresentanteFiscaleViewModel _rappresentanteFiscaleViewModel;
        private DatiTerzoIntermediarioViewModel _terzoIntermediarioViewModel;
        private DatiRiepilogoIvaViewModel _datiRiepilogoIvaViewModel;
        private string _domainResultFatturaPa;
        private string _iconXsdValidationState;


        #endregion
    }
}
