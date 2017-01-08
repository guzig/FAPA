using System;
using System.Collections;
using System.ComponentModel;
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

        public RitenutaTabViewModel RitenutaTabViewModel { get; set; }

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

            DettagliFatturaViewModel = new DettagliFatturaViewModel(this, fattura);
            DettagliFatturaViewModel.Init();
            DettagliFatturaViewModel.CurrentEntityChanged += OnDettaglioFatturaPropertyChanged;

            DatiPagamentoViewModel = new DatiPagamentoTabViewModel( this, fattura );
            DatiPagamentoViewModel.Init();
            AddTabViewModel<DatiPagamentoTabViewModel>( DatiPagamentoViewModel );

            TrasmittenteViewModel = new TrasmittenteTabViewModel(this, fattura);
            TrasmittenteViewModel.Init();
            AddTabViewModel<TrasmittenteTabViewModel>( TrasmittenteViewModel );

            if (fattura?.Ritenuta != null)
            {
                AddTabRitenuta();
            }

            if ( fattura?.DatiOrdineAcquisto != null )
            {
                AddTabOrdine();
            }

            if (fattura?.DatiContratto != null)
            {
                AddTabContratto();
            }

            if (fattura?.DatiConvenzione != null)
            {
                AddTabConvenzione();
            }

        }

        private void OnDettaglioFatturaPropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private ICommand _addConvenzioneCommand;
        public ICommand AddConvenzioneCommand
        {
            get
            {
                if (_addConvenzioneCommand != null) return _addConvenzioneCommand;
                _addConvenzioneCommand = new RelayCommand(param => AddConvenzione(), r => true);
                return _addConvenzioneCommand;
            }
        }

        private void AddConvenzione()
        {
            if (CurrentEntity == null) return;
            var ordineTabViewModel = AddTabConvenzione();
            BasePresenter.SetActiveWorkSpace(BasePresenter.Workspaces.IndexOf(ordineTabViewModel));
        }

        private DatiConvenzioneTabViewModel AddTabConvenzione()
        {
            var datiOrdine = new DatiConvenzioneTabViewModel(this, CurrentEntity);
            datiOrdine.Init();
            AddTabViewModel<DatiConvenzioneTabViewModel>(datiOrdine);
            return datiOrdine;
        }

        private ICommand _addContrattoCommand;
        public ICommand AddContrattoCommand
        {
            get
            {
                if (_addContrattoCommand != null) return _addContrattoCommand;
                _addContrattoCommand = new RelayCommand(param => AddContratto(), r => true);
                return _addContrattoCommand;
            }
        }

        private void AddContratto()
        {
            if (CurrentEntity == null) return;
            var ordineTabViewModel = AddTabContratto();
            BasePresenter.SetActiveWorkSpace(BasePresenter.Workspaces.IndexOf(ordineTabViewModel));
        }

        private DatiContrattoTabViewModel AddTabContratto()
        {
            var datiOrdine = new DatiContrattoTabViewModel(this, CurrentEntity);
            datiOrdine.Init();
            AddTabViewModel<DatiContrattoTabViewModel>(datiOrdine);
            return datiOrdine;
        }

        private ICommand _addOrdineCommand;
        public ICommand AddOrdineCommand
        {
            get
            {
                if ( _addOrdineCommand != null ) return _addOrdineCommand;
                _addOrdineCommand = new RelayCommand( param => AddOrdine(), r=>true );
                return _addOrdineCommand;
            }
        }

        private DatiOrdineTabViewModel AddTabOrdine()
        {
            var datiOrdine = new DatiOrdineTabViewModel( this, CurrentEntity );
            datiOrdine.Init();
            AddTabViewModel<DatiOrdineTabViewModel>( datiOrdine );
            return datiOrdine;
        }

        private void AddOrdine()
        {
            if ( CurrentEntity == null ) return;

            var ordineTabViewModel = AddTabOrdine();
            //ordineTabViewModel.IsEditing = false;
            //ordineTabViewModel.LockMessage = OnEditingLockMessage;

            BasePresenter.SetActiveWorkSpace( BasePresenter.Workspaces.IndexOf( ordineTabViewModel ) );
        }

        private ICommand _addRitenutaCommand;
        public ICommand AddRitenutaCommand
        {
            get
            {
                if ( _addRitenutaCommand != null) return _addRitenutaCommand;
                _addRitenutaCommand = new RelayCommand(param => AddRitenuta(), CanAddRitenuta);
                return _addRitenutaCommand;
            } 
        }

        private void AddRitenuta()
        {
            if ( CurrentEntity == null ) return;
            
            var currentViewModel = BasePresenter.Workspaces.FirstOrDefault(t => t is RitenutaTabViewModel);
            if (currentViewModel != null)
            {
                BasePresenter.SetActiveWorkSpace(BasePresenter.Workspaces.IndexOf( currentViewModel ) );
                return;
            }

            var ritenutaTabViewModel = AddTabRitenuta();
            BasePresenter.SetActiveWorkSpace( BasePresenter.Workspaces.IndexOf( ritenutaTabViewModel ) );
        }

        private RitenutaTabViewModel AddTabRitenuta()
        {
            RitenutaTabViewModel = new RitenutaTabViewModel( this, CurrentEntity);
            RitenutaTabViewModel.Init();
            AddTabViewModel<RitenutaTabViewModel>(RitenutaTabViewModel);
            return RitenutaTabViewModel;
        }
        
        private void AddTabViewModel<TV>(WorkspaceViewModel tabViewModel )
        {
            // Dispatcher.CurrentDispatcher.BeginInvoke(new System.Action(() =>
            //{
            //    BasePresenter.Workspaces.Add( _ritenutaViewModel = ritenutaViewModel );          
            //} ));
            var vm = BasePresenter.Workspaces.FirstOrDefault( a => a is TV );
            if ( vm == null )
                BasePresenter.Workspaces.Add( tabViewModel );
        }

        private static bool CanAddRitenuta(object param)
        {
            return true;
            //return CurrentEntity?.Ritenuta == null;
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
            FlushXml( fattura );
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

        private void OnOpendXml()
        {
            if (CurrentEntity == null) return;
            Presenters.Show("ShowXmlToTreeView", CurrentEntity.GetXmlDocument());
        }

        private void FlushXml( IValidatable fattura )
        {
            //ShowCursor.Show();
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var nomeFile = CurrentEntity.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente.IdPaese +
                           CurrentEntity.AnagraficaCedenteDB.CodiceFiscale + "_" +
                           CurrentEntity.FatturaPa.FatturaElettronicaHeader.DatiTrasmissione.ProgressivoInvio + ".xml";
            var outPath = Path.Combine( folderPath, nomeFile );
                
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //ShowCursor.Show();
                    var errors = fattura.Validate();
                    if ( !errors.Success )
                        return "Fattura non validata: " + Environment.NewLine + string.Join( "; ", errors );
                    var xmlDoc = CurrentEntity.GetXmlDocument();
                    xmlDoc.Save( outPath );
                    return null;
                }
                catch (Exception )
                {
                    return "Errore inaspettato durante la generazione della fattura";
                }
            }).ContinueWith(obj =>
            {
                var result = (string)obj.Result;
                if (string.IsNullOrWhiteSpace(result))
                {
                    var msg = "Fattura validata." + Environment.NewLine + "Il file " + nomeFile + " è stato salvato sul desktop";
                    MessageBox.Show(msg, "Salvataggio completato", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(result, "Fattura non generata...", MessageBoxButton.OK);
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());

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
