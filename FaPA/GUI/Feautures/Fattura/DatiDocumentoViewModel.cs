using FaPA.Core.FaPa;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiDocumentoViewModel : WorkspaceViewModel
    {
        #region fields
        private int _tabIndex = 0;
        private DatiBolloViewModel _datiBolloViewModel;
        private DatiRitenutaViewModel _ritenutaTabViewModel;
        private ScontoMaggiorazioneGeneraleViewModel _scontoMaggiorazioneGeneraleView;
        private DatiGeneraliDocumentoViewModel _datiGeneraliDocumentoViewModel;
        private DatiCassaPrevViewModel _datiCassaPrevViewModel;

        #endregion

        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                if (value == _tabIndex) return;
                _tabIndex = value;
                NotifyOfPropertyChange(() => TabIndex);
            }
        }

        public DatiRitenutaViewModel DatiRitenutaViewModel
        {
            get { return _ritenutaTabViewModel; }
            set
            {
                if ( Equals( value, _ritenutaTabViewModel ) ) return;
                _ritenutaTabViewModel = value;
                NotifyOfPropertyChange( () => DatiRitenutaViewModel );
            }
        }
        
        public DatiBolloViewModel DatiBolloViewModel
        {
            get { return _datiBolloViewModel; }
            set
            {
                _datiBolloViewModel = value;
                NotifyOfPropertyChange( () => DatiBolloViewModel );
            }
        }

        public DatiCassaPrevViewModel DatiCassaPrevViewModel
        {
            get { return _datiCassaPrevViewModel; }
            set
            {
                if ( Equals( value, _datiCassaPrevViewModel ) ) return;
                _datiCassaPrevViewModel = value;
                NotifyOfPropertyChange( () => DatiCassaPrevViewModel );
            }
        }

        public ScontoMaggiorazioneGeneraleViewModel ScontoMaggiorazioneGeneraleView
        {
            get { return _scontoMaggiorazioneGeneraleView; }
            set
            {
                if (Equals(value, _scontoMaggiorazioneGeneraleView)) return;
                _scontoMaggiorazioneGeneraleView = value;
                NotifyOfPropertyChange(() => ScontoMaggiorazioneGeneraleView);
            }
        }

        public DatiGeneraliDocumentoViewModel DatiGeneraliDocumentoViewModel
        {
            get { return _datiGeneraliDocumentoViewModel; }
            set
            {
                if ( Equals( value, _datiGeneraliDocumentoViewModel ) ) return;
                _datiGeneraliDocumentoViewModel = value;
                NotifyOfPropertyChange( () => DatiGeneraliDocumentoViewModel );
            }
        }

        //ctor
        public DatiDocumentoViewModel(IRepository repository, DatiGeneraliType instance, int tabIndex)
        {
            TabIndex = tabIndex;

            DisplayName = "Dati documento";
            IsCloseable = false;

            DatiGeneraliDocumentoViewModel = new DatiGeneraliDocumentoViewModel(repository, instance);
            DatiGeneraliDocumentoViewModel.Init();

            ScontoMaggiorazioneGeneraleView = new ScontoMaggiorazioneGeneraleViewModel(repository, 
                instance.DatiGeneraliDocumento);

            ScontoMaggiorazioneGeneraleView.Init();
           
            DatiBolloViewModel = new DatiBolloViewModel( repository, instance.DatiGeneraliDocumento );
            DatiBolloViewModel.Init();

            DatiCassaPrevViewModel = new DatiCassaPrevViewModel( repository, instance.DatiGeneraliDocumento );
            DatiCassaPrevViewModel.Init();

            DatiRitenutaViewModel = new DatiRitenutaViewModel(repository, instance.DatiGeneraliDocumento);
            DatiRitenutaViewModel.Init();

        }

    }
}