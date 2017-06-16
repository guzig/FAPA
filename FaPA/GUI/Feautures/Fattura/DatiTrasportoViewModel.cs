using System.ComponentModel;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiTrasportoViewModel : CrudViewModel<DatiGeneraliType, DatiTrasportoType>
    {
        private DatiAnagraficiVettoreViewModel _datiAnagraficiViewModel;
        private DatiIndirizzoViewModel _datiIndirizzoViewModel;

        public DatiAnagraficiVettoreViewModel DatiAnagraficiVettoreViewModel
        {
            get { return _datiAnagraficiViewModel; }
            set
            {
                if ( Equals( value, _datiAnagraficiViewModel ) ) return;
                _datiAnagraficiViewModel = value;
                NotifyOfPropertyChange( () => DatiAnagraficiVettoreViewModel );
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
            DatiAnagraficiVettoreViewModel = new DatiAnagraficiVettoreViewModel( Repository, Instance.DatiTrasporto );
            DatiIndirizzoViewModel = new DatiIndirizzoViewModel( Repository, Instance.DatiTrasporto );

            if ( Instance.DatiTrasporto != null )
            {
                DatiAnagraficiVettoreViewModel.Init();
                DatiIndirizzoViewModel.Init();
            }

            DatiAnagraficiVettoreViewModel.CurrentEntityPropChanged += OnChildViewModelPropChanged;
            DatiIndirizzoViewModel.CurrentEntityPropChanged += OnChildViewModelPropChanged;
        }

        private void OnChildViewModelPropChanged( object sender, PropertyChangedEventArgs eventarg )
        {
            if ( !( sender is BaseEntity ) ) return ;

            var validatable = ( IValidatable ) CurrentPoco;

            validatable.Validate();

            validatable.HandleValidationResults();

            ( (IValidatable) CurrentPoco ).HandleValidationResults( "DatiTrasporto" );

            if ( sender is DatiAnagraficiVettoreType || sender is AnagraficaType)
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

        public override void AddEntity()
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