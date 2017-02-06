using System.ComponentModel;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiTrasportoViewModel : EditWorkSpaceViewModel<DatiGeneraliType, DatiTrasportoType>
    {
        private DatiAnagraficiViewModel _datiAnagraficiViewModel;
        private DatiIndirizzoViewModel _datiIndirizzoViewModel;

        public DatiAnagraficiViewModel DatiAnagraficiViewModel
        {
            get { return _datiAnagraficiViewModel; }
            set
            {
                if ( Equals( value, _datiAnagraficiViewModel ) ) return;
                _datiAnagraficiViewModel = value;
                NotifyOfPropertyChange( () => DatiAnagraficiViewModel );
            }
        }

        public DatiIndirizzoViewModel DatiIndirizzoViewModel
        {
            get { return _datiIndirizzoViewModel; }
            set
            {
                if ( Equals( value, _datiIndirizzoViewModel ) ) return;
                _datiIndirizzoViewModel = value;
                NotifyOfPropertyChange( () => DatiIndirizzoViewModel );
            }
        }

        //ctor
        public DatiTrasportoViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( repository, instance, f => f.DatiTrasporto, "Trasporto", true )
        {
            InitChildViewModel();
        }

        private void InitChildViewModel()
        {
            DatiAnagraficiViewModel = new DatiAnagraficiViewModel( Repository, Instance.DatiTrasporto );
            DatiIndirizzoViewModel = new DatiIndirizzoViewModel( Repository, Instance.DatiTrasporto );

            if ( Instance.DatiTrasporto != null )
            {
                DatiAnagraficiViewModel.Init();
                DatiIndirizzoViewModel.Init();
            }

            DatiAnagraficiViewModel.CurrentEntityChanged += OnChildViewModelChanged;
            DatiIndirizzoViewModel.CurrentEntityChanged += OnChildViewModelChanged;
        }

        private void OnChildViewModelChanged( object sender, PropertyChangedEventArgs eventarg )
        {
            if ( !( sender is BaseEntity ) ) return ;

            var validatable = ( IValidatable ) CurrentPoco;

            validatable.Validate();

            validatable.HandleValidationResults();

            ( (IValidatable) CurrentPoco ).HandleValidationResults( "DatiTrasporto" );

            if ( sender is DatiAnagraficiVettoreType )
            {
                ( ( CurrentPoco as DatiTrasportoType ).DatiAnagraficiVettore as IValidatable ).
                    HandleValidationResults( "DatiAnagraficiVettore" );
            }

            if ( sender is IndirizzoType )
            {
                ( ( CurrentPoco as DatiTrasportoType ).IndirizzoResa as IValidatable ).
                    HandleValidationResults( "IndirizzoResa" );
            }


            ProcessChangedEvent( CurrentPoco );
        }

        public override DatiGeneraliType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( Core.Fattura ) root ).DatiGenerali;
        }

        protected override void AddEntity()
        {
            base.AddEntity();
            InitChildViewModel();
        }

        protected override bool CanAddEntity( object obj )
        {
            return Instance != null;
        }
    }
}