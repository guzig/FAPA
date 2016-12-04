using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.GUI.Utils;
using FaPA.Infrastructure.Dto;
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
                if ( Equals( value, _dettagliFatturaViewModel ) ) return;
                _dettagliFatturaViewModel = value;
                NotifyOfPropertyChange( () => DettagliFatturaViewModel );
            }
        }

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
            var instance = base.CreateInstance();
            instance.Init();

            object currentHeader = instance.FatturaElettronicaHeader;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>(ref currentHeader, "FaPA.Core");
            instance.FatturaElettronicaHeader = (FaPA.Core.FaPa.FatturaElettronicaHeaderType)currentHeader;

            object currentBody = instance.FatturaElettronicaBody;
            ObjectExplorer.TryProxiedAllInstances<FaPA.Core.BaseEntityFpa>(ref currentBody, "FaPA.Core");
            instance.FatturaElettronicaBody = (FaPA.Core.FaPa.FatturaElettronicaBodyType)currentBody;

            return instance;
        }

        protected override bool TrySaveCurrentEntity()
        {
            //var header = ObjectExplorer.UnProxiedDeep( CurrentEntity.FatturaPa.FatturaElettronicaHeader );
            //CurrentEntity.FatturaElettronicaHeader = ( FatturaElettronicaHeaderType ) header;

            //var body = ObjectExplorer.UnProxiedDeep( CurrentEntity.FatturaPa.FatturaElettronicaBody );
            //CurrentEntity.FatturaElettronicaBody = ( FatturaElettronicaBodyType ) body;

            CurrentEntity.SetTrasmittente();

            return base.TrySaveCurrentEntity();
        }

        protected override void Dispose()
        {
            CurrentEntityChanged -= OnCurrentFatturaChanged;
            base.Dispose();
        }

        protected override void DefaultCancelOnEditAction()
        {
           base.DefaultCancelOnEditAction();
           InitFatturaTabs();
        }

        #endregion

        public override void OnPageGotFocus()
        {
            base.OnPageGotFocus();
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

            if (fattura?.Ritenuta != null )
            {
                AddTabRitenuta();
            }

            //DettagliFatturaViewModel = new DettagliFatturaViewModel(this, fattura);
            //DettagliFatturaViewModel.Init<DettaglioLineeType, DettaglioLineeType>();
            //DettagliFatturaViewModel.CurrentEntityChanged += OnDettaglioFatturaPropertyChanged;

            var datiPagamento = new DatiPagamentoTabViewModel( this, fattura );
            datiPagamento.Init<DatiPagamentoType, DatiPagamentoDto>();
            AddTabViewModel<DatiPagamentoTabViewModel>( datiPagamento );

            var trasmittente = new TrasmittenteTabViewModel(this, fattura);
            trasmittente.Init<DatiTrasmissioneType, DatiTrasmissioneDto>();
            AddTabViewModel<TrasmittenteTabViewModel>(trasmittente);

        }

        //protected virtual void OnCurrentChanged(object sender, EventArgs e)

        private void OnDettaglioFatturaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            IsInEditing = true;
            AllowSave = IsValidate();
        }
        
        private bool IsValidate()
        {
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
            datiOrdine.Init<DatiDocumentiCorrelatiType, DatiOrdineAcquistoDto>();
            AddTabViewModel<DatiOrdineTabViewModel>( datiOrdine );
            return datiOrdine;
        }

        private void AddOrdine()
        {
            if ( CurrentEntity == null ) return;

            var ordineTabViewModel = AddTabOrdine();
            //ordineTabViewModel.IsInEditing = false;
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
            var ritenutaTabViewModel = new RitenutaTabViewModel( this, CurrentEntity);
            ritenutaTabViewModel.Init<DatiRitenutaType, DatiRitenutaTypeDto>();
            AddTabViewModel<RitenutaTabViewModel>(ritenutaTabViewModel);
            return ritenutaTabViewModel;
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

        private bool CanAddRitenuta(object param)
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

        private void ValidateAndFlushXml(Core.Fattura fattura)
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
