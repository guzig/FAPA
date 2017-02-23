using FaPA.Core.FaPa;
using FaPA.GUI.Controls.MyTabControl;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliViewModel : WorkspaceViewModel
    {
        #region fields
        private DatiFatturaPrincipaleViewModel _datiFatturaPrincipaleViewModel;
        private DatiTrasportoViewModel _datiTrasportoViewModel;
        private DatiSalTabViewModel _datiSalViewModel;
        private DatiDdtTabViewModel _datiDdtViewModel;
        private DatiOrdineTabViewModel _datiOrdini;
        private DatiContrattoTabViewModel _datiContratto;
        private DatiConvenzioneTabViewModel _datiConvenzione;
        private DatiFattureCollegateTabViewModel _datiFattureCollegate;
        private DatiRicezioneTabViewModel _datiRicezione;
        private int _tabIndex=0;

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

        public DatiTrasportoViewModel DatiTrasportoViewModel
        {
            get { return _datiTrasportoViewModel; }
            set
            {
                if ( Equals( value, _datiTrasportoViewModel ) ) return;
                _datiTrasportoViewModel = value;
                NotifyOfPropertyChange( () => DatiTrasportoViewModel );
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


        //ctor
        public DatiGeneraliViewModel( IRepository repository, FatturaElettronicaBodyType instance, int tabIndex ) 
        {
            DisplayName = "Dati generali";
            IsCloseable = false;
            InitChildViewModels(instance, repository);
            TabIndex = tabIndex;
        }

        private void InitChildViewModels( FatturaElettronicaBodyType instance, IRepository repository )
        {
            DatiFatturaPrincipaleViewModel = new DatiFatturaPrincipaleViewModel(repository, instance.DatiGenerali);
            DatiFatturaPrincipaleViewModel.Init();

            DatiTrasportoViewModel = new DatiTrasportoViewModel(repository, instance.DatiGenerali);
            DatiTrasportoViewModel.Init();

            DatiDdtViewModel = new DatiDdtTabViewModel(repository, instance.DatiGenerali);
            DatiDdtViewModel.Init();

            DatiSalViewModel = new DatiSalTabViewModel( repository, instance.DatiGenerali);
            DatiSalViewModel.Init();

            DatiOrdini = new DatiOrdineTabViewModel(repository, instance.DatiGenerali);
            DatiOrdini.Init();

            DatiContratto = new DatiContrattoTabViewModel(repository, instance.DatiGenerali);
            DatiContratto.Init();

            DatiConvenzione = new DatiConvenzioneTabViewModel(repository, instance.DatiGenerali);
            DatiConvenzione.Init();

            DatiFattureCollegate = new DatiFattureCollegateTabViewModel(repository, instance.DatiGenerali);
            DatiFattureCollegate.Init();

            DatiRicezione = new DatiRicezioneTabViewModel(repository, instance.DatiGenerali);
            DatiRicezione.Init();
        }
      
    }
}