using System;
using System.ComponentModel;
using System.Diagnostics;
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
    public class DatiDocumentoViewModel : EditWorkSpaceViewModel<DatiGeneraliType, DatiGeneraliDocumentoType>
    {
        private DatiRitenutaViewModel _ritenutaTabViewModel;
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

        private ScontoMaggiorazioneGeneraleViewModel _scontoMaggiorazioneGeneraleView;
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

        //ctor
        public DatiDocumentoViewModel(IRepository repository, DatiGeneraliType instance ) :
            base(repository, instance, f => f.DatiGeneraliDocumento, "Dati documento", false)
        {
            ScontoMaggiorazioneGeneraleView = new ScontoMaggiorazioneGeneraleViewModel(repository, 
                instance.DatiGeneraliDocumento);

            ScontoMaggiorazioneGeneraleView.Init();

            DatiRitenutaViewModel = new DatiRitenutaViewModel( repository, instance.DatiGeneraliDocumento );
            DatiRitenutaViewModel.CurrentEntityChanged += OnDatiRitenutaPropertyChanged;
        }

        private void OnDatiRitenutaPropertyChanged( object sender, PropertyChangedEventArgs eventarg )
        {
            LockMessage = EditViewModel<BaseEntity>.OnEditingLockMessage;
            AllowSave = DatiRitenutaViewModel.IsValid;
        }

        protected override bool CanDeleteEntity( object obj )
        {
            return false;
        }


        protected override void HookOnChanged(object poco)
        {
            var entity = poco as DatiGeneraliDocumentoType;
            if (entity == null) return;

            HookChanged(entity);

            if (entity.DatiRitenuta != null)
            {
                HookChanged( entity.DatiRitenuta );
            }

            if (entity.DatiBollo != null)
            {
                HookChanged(entity.DatiBollo);
            }

            if (entity.DatiCassaPrevidenziale != null)
            {
                HookChanged(entity.DatiCassaPrevidenziale);
            }

            if (entity.ScontoMaggiorazione != null)
            {
                foreach (var dettaglio in entity.ScontoMaggiorazione)
                {
                    HookChanged(dettaglio);
                }
            }


        }

        protected override void OnRequestClose()
        {
            if (UserProperty != null)
            {
                const string lockMessage = "Non è possibile chiudere una scheda contenente dati.";
                MessageBox.Show(lockMessage, "Scheda bloccata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            base.OnRequestClose();
        }

        public override object Read()
        {
            var root = Repository.Read();
            Instance = ( ( Core.Fattura ) root ).DatiGenerali;
            var userProp = GetterProp( Instance );
            return userProp;
        }

    }
}